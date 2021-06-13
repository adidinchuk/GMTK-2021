using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    private float baseGoalDistance = 15f;
    private float goalDistanceIncrement = 10f;

    private int baseGoalScore = 300;
    private int goalScoreIncrement = 150;

    private int level = 1;

    private void Start()
    {


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
        gameUI.SetGoal(goal.transform.position);
        gameUI.SetMainShip(mainShip);


        // Register for events
        mainShipInstance.GetComponent<Health>().OnDied += LevelController_OnDied;
    }


    private void Update()
    {
        if (Input.GetKeyDown("space"))
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
        Debug.Log("Level Lost!");
        LevelLost();
    }


    public void LevelLost()
    {
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);
    }

    public void LevelWon()
    {
        // Destroy all attached objects
        mainShip.Jettison();

        // Spawn next planet
        Goal.Create(mainShipInstance.transform.position, baseGoalDistance + (goalDistanceIncrement * level), baseGoalScore + (goalScoreIncrement * level));
        
        // Increase difficulty
        level++;


        // TODO: Increase spawn rates on spawners?
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
