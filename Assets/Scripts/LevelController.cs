using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;

    public ShipPart mainShipPrefab;
    public Spawner spawnerPrefab;
    public Goal goalPrefab;
    public GameUI gameUI;

    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    private GameObject goalInstance;
    private GameObject mainShipInstance;
    private GameObject spawnerInstance;


    private void Start()
    {
        // instantiate a victory location
        goalInstance = Instantiate(goalPrefab.gameObject);

        // Spawn main ship
        mainShipInstance = Instantiate(mainShipPrefab.gameObject);
        vcam.Follow = mainShipInstance.transform;

        // Start spawners
        spawnerInstance = Instantiate(spawnerPrefab.gameObject);
        spawnerInstance.transform.SetParent(mainShipInstance.transform);

        // Spawn stars / Background?


        // Setup UI
        gameUI.SetGoal(goalInstance.transform.position);
        gameUI.SetMainShip(mainShipInstance.GetComponent<MainShip>());

        // Hide menues
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);

        // Register for events
        goalInstance.GetComponent<Goal>().OnGoalReached += LevelController_GoalReached;
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
        Debug.Log("Level Won!");
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
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);
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
