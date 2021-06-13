using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;

    public ShipPart mainShipPrefab;
    public Spawner meteorSpawnerPrefab;
    public Spawner shipPartSpawnerPrefab;
    public Goal goalPrefab;
    public GameUI gameUI;

    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    private Transform goalTransform;
    private MainShip mainShip;

    private GameObject mainShipInstance;
    private GameObject meteorSpawnerInstance;
    private GameObject shipPartSpawnerInstance;
    public TextMeshProUGUI finalScore;

    private float baseGoalDistance = 15f;
    private float goalDistanceIncrement = 10f;

    private int baseGoalScore = 300;
    private int goalScoreIncrement = 150;

    private int level = 1;

    [SerializeField]
    AudioClip BGM;

    private void Start()
    {
        MusicManager instance = MusicManager.getInstance();
        if (instance != null)
        {
            instance.updateMusic(BGM);
        }

        // Spawn main ship
        mainShipInstance = Instantiate(mainShipPrefab.gameObject);
        mainShip = mainShipInstance.GetComponent<MainShip>();
        vcam.Follow = mainShipInstance.transform;

        // Start spawners
        meteorSpawnerInstance = Instantiate(meteorSpawnerPrefab.gameObject);
        meteorSpawnerInstance.transform.SetParent(mainShipInstance.transform);

        shipPartSpawnerInstance = Instantiate(shipPartSpawnerPrefab.gameObject);
        shipPartSpawnerInstance.transform.SetParent(mainShipInstance.transform);

        // Spawn stars / Background?


        // instantiate a victory location
        Goal goal = Goal.Create(mainShipInstance.transform.position, 10f, baseGoalScore);
        goal.OnGoalReached += LevelController_GoalReached;

        // Setup UI
        gameUI.SetGoal(goal);
        gameUI.SetMainShip(mainShip);


        // Register for events
        mainShipInstance.GetComponent<Health>().OnDied += LevelController_OnDied;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();

        }
    }

    private void LevelController_GoalReached(object sender, System.EventArgs e)
    {
        LevelWon();
    }
    private void LevelController_OnDied(object sender, System.EventArgs e)
    {
        LevelLost();
    }


    public void LevelLost()
    {
        Time.timeScale = 0;
        int totalWeightDelivered = mainShip.GetTotalWeightDelivered();
        string scoreMsg = totalWeightDelivered > 0 ? $"{totalWeightDelivered} Kg Delivered" : "None...";

        //gameOverMenu.transform.Find("scoreText").GetComponent<TextMeshProUGUI>().SetText(scoresg);

        finalScore.SetText(scoreMsg);

        gameOverMenu.SetActive(true);
    }

    public void LevelWon()
    {
        // Destroy all attached objects
        mainShip.Jettison();

        // Spawn next planet
        Goal goal = Goal.Create(mainShipInstance.transform.position, baseGoalDistance + (goalDistanceIncrement * level), baseGoalScore + (goalScoreIncrement * level));
        gameUI.SetGoal(goal);

        // Increase difficulty
        level++;
        meteorSpawnerPrefab.minSpawnDelay -= 0.5f;
        meteorSpawnerPrefab.maxSpawnDelay -= 1;
        // TODO: Increase spawn rates on spawners?
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }


    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }


}
