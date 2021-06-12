using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{

    public static GameUI Instance { get; private set; }
    public float goalIndicatorRadius = 300f;

    private Vector3 goalPosition;
    private RectTransform goalPositionIndicator;
    private TextMeshProUGUI goalDistanceText;
    private TextMeshProUGUI scoreText;

    private float checkScoreTimer = 0f;
    private float checkScoreTimerMax = 0.5f;

    private MainShip mainShip;

    void Awake()
    {
        Instance = this;
        goalPositionIndicator = transform.Find("goalPositionIndicator").GetComponent<RectTransform>();
        goalDistanceText = transform.Find("distanceText").GetComponent<TextMeshProUGUI>();
        scoreText = transform.Find("scoreText").GetComponent<TextMeshProUGUI>();
    }


    public void SetMainShip(MainShip mainShip)
    {
        this.mainShip = mainShip;
    }
    public void SetGoal(Vector3 goalPosition)
    {
        this.goalPosition = goalPosition;
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
            int score = mainShip.GetScore();
            scoreText.SetText(score.ToString() + " kg");
            checkScoreTimer += checkScoreTimerMax; 
        }

    }
}
