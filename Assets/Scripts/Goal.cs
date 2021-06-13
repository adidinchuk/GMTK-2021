using System;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public event EventHandler OnGoalReached;
    
    private int targetWeight;
    private bool goalReached = false;

    bool overlappingWithMainShip = false;
    private MainShip mainShip;



    [SerializeField] Sprite[] sprites;
    private SpriteRenderer spriteRenderer;


    private float checkShipCargoTimerMax = 0.2f;
    private float checkShipCargoTimer = 0.0f;

    // TODO: Add a visual for the current goal
    public static Goal Create(Vector3 position, float radius, int targetWeight) {
        var angle = UnityEngine.Random.Range(0, 1f) * Mathf.PI * 2;

        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        Vector3 spawnPosition = new Vector3(x + position.x, y + position.y);

        Transform pfGoal = Resources.Load<Transform>("pfGoal");
        Transform goalTransform = Instantiate(pfGoal, spawnPosition, Quaternion.identity);

        Goal goal = goalTransform.GetComponent<Goal>();
        goal.SetTargetWeight(targetWeight);
        return goal;
    }

    public void SetTargetWeight(int targetWeight)
    {
        this.targetWeight = targetWeight;
        this.goalReached = false;
    }

    public void Awake()
    {
       
        if (sprites.Length > 0) {

            spriteRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();
       
            spriteRenderer.sprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];

            float colliderRadius = spriteRenderer.bounds.size.x / 2;

            CircleCollider2D collider = transform.GetComponent<CircleCollider2D>();
            collider.radius = colliderRadius;
        } 
        else
        {
            Debug.LogWarning("No sprites set for goal: " + name);
        }

    }


    public void Update()
    {
        
        if (overlappingWithMainShip) {
            checkShipCargoTimer -= Time.deltaTime;
            if (checkShipCargoTimer <= 0) { 
                checkShipCargoTimer += checkShipCargoTimerMax;
                CheckCargo();
            }
        }
    }

    public int GetTargetWeight()
    {
        return this.targetWeight;
    }



    void OnTriggerEnter2D(Collider2D col)
    {
        MainShip tempShip = col.gameObject.GetComponent<MainShip>();

         if (tempShip != null)
        {
            mainShip = tempShip;
            overlappingWithMainShip = true;
            CheckCargo();
        }

  
   
    }
    void OnTriggerExit2D(Collider2D col)
    {
        MainShip mainShip = col.gameObject.GetComponent<MainShip>();
        
        if (mainShip != null)
        {
            overlappingWithMainShip = false;
            mainShip = null;
        }
    }

    void CheckCargo()
    {
        if (goalReached)
        {
            Debug.Log("Already reached this goal");
            return;
        }

        

        int currentWeight = mainShip.GetCarriedWeight();

        if (currentWeight >= targetWeight)
        {
            goalReached = true;
            OnGoalReached?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Debug.Log("Current weight of ship: " + currentWeight);
            Debug.Log("Score too low, required: " + targetWeight);
        }
    }
}
