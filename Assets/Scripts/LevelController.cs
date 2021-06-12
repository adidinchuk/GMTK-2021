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
      
    }
    // Start is called before the first frame update
    void Start()
    {
        // instantiate a victory location
        GameObject goalInstance = Instantiate(goal.gameObject);
        GameUI.Instance.SetGoal(goalInstance.transform.position);

        // Spawn main ship
        GameObject mainShipInstance = Instantiate(mainShip.gameObject);
        vcam.Follow = mainShipInstance.transform;


        // Start spawners
        Instantiate(spawner.gameObject);

        // Spawn stars / Background?








    }



}
