using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    public ShipPart mainShip;
    public Spawner spawner;
    public Goal goal;
    public CinemachineVirtualCamera vcam;


    private void Awake()
    {
        PlayerPrefs.SetFloat("EffectsVolume", 1f);
    }
    // Start is called before the first frame update
    void Start()
    {
        // instantiate a victory location
        GameObject goalInstance = Instantiate(goal.gameObject);

        // Spawn main ship
        GameObject mainShipInstance = Instantiate(mainShip.gameObject);
        vcam.Follow = mainShipInstance.transform;

        // Start spawners
        GameObject spawnerInstance = Instantiate(spawner.gameObject);
        spawnerInstance.transform.SetParent(mainShipInstance.transform);

        // Spawn stars / Background?


        // Setup UI
        GameUI.Instance.SetGoal(goalInstance.transform.position);
        GameUI.Instance.SetMainShip(mainShipInstance.GetComponent<MainShip>());



    }



}
