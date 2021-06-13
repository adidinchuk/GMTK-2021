using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{

    public float goalIndicatorRadius = 300f;

    private Goal goal;
    private Vector3 goalPosition;
    private RectTransform goalPositionIndicator;
    private TextMeshProUGUI goalDistanceText;
    private TextMeshProUGUI weightText;
    private TextMeshProUGUI scoreText;

    private float checkScoreTimer = 0f;
    private float checkScoreTimerMax = 0.2f;

    private MainShip mainShip;

    void Awake()
    {
        goalPositionIndicator = transform.Find("goalPositionIndicator").GetComponent<RectTransform>();
        goalDistanceText = transform.Find("distanceText").GetComponent<TextMeshProUGUI>();
        weightText = transform.Find("weightText").GetComponent<TextMeshProUGUI>();
        scoreText = transform.Find("scoreText").GetComponent<TextMeshProUGUI>();
    }


    public void SetMainShip(MainShip mainShip)
    {
        this.mainShip = mainShip;
    }
    public void SetGoal(Goal goal)
    {
        this.goal = goal;
        this.goalPosition = goal.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (goalPosition == null || mainShip == null) return;
        Vector3 directionToGoal = (goalPosition - Camera.main.transform.position).normalized;
        goalPositionIndicator.anchoredPosition = directionToGoal * goalIndicatorRadius;
        goalPositionIndicator.eulerAngles = new Vector3(0, 0, Utils.GetAngleFromVector(directionToGoal));

        float distanceToGoal = Vector2.Distance(goalPosition, Camera.main.transform.position)  ;
        goalDistanceText.SetText(distanceToGoal.ToString("F1") + " km");

        checkScoreTimer -= Time.deltaTime;

        if (checkScoreTimer <= checkScoreTimerMax)
        {
            int totalWeightDelivered = mainShip.GetTotalWeightDelivered();
            int carriedWeight = mainShip.GetCarriedWeight();
            int targetWeight = goal.GetTargetWeight();

            string weightMsg = $"{carriedWeight} Kg / {targetWeight} Kg";
            string scoreMsg = totalWeightDelivered > 0 ? $"{totalWeightDelivered} Kg Delivered" : "";

            weightText.SetText(weightMsg);
            scoreText.SetText(scoreMsg);
            checkScoreTimer += checkScoreTimerMax; 
        }

    }
}
