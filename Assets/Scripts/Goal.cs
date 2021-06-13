using System;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public event EventHandler OnGoalReached;
    
    private int targetWeight;
    private bool goalReached = false;

    [SerializeField] Sprite[] sprites;
    private SpriteRenderer spriteRenderer;


    // TODO: Add a visual for the current goal
    public static Goal Create(Vector3 position, float radius, int targetWeight) {
        var angle = UnityEngine.Random.Range(0, 1f) * Mathf.PI * 2;

        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        Vector3 spawnPosition = new Vector3(x + position.x, y + position.y);

        Transform pfGoal = Resources.Load<Transform>("pfGoal");
        Transform goalTransform = Instantiate(pfGoal, spawnPosition, Quaternion.identity);

        Goal goal = goalTransform.GetComponent<Goal>();
       goal.targetWeight = targetWeight;

        return goal;
    }

    public void Awake()
    {
        if (sprites.Length > 0) {

            spriteRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];
        } 
        else
        {
            Debug.LogWarning("No sprites set for goal: " + name);
        }

    }


    public int GetTargetWeight()
    {
        return this.targetWeight;
    }



    void OnTriggerEnter2D(Collider2D col)
    {
        MainShip mainShip = col.gameObject.GetComponent<MainShip>();
        if (mainShip == null || goalReached) return;

        // if col is our ship
        if (mainShip.GetCarriedWeight() >= targetWeight) { 
            goalReached = true;
            OnGoalReached?.Invoke(this, EventArgs.Empty);
        } else {
            Debug.Log("Score too low, required: " + targetWeight);
            
                    }
    }

}
