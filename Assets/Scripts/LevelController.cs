using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    public ShipPart mainShip;
    public GameObject shipPartSpawner;
    public GameObject meteorSpawner;
    public GameObject backgroundSpawner;
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



        // Spawn stars

        // Start spawners






    }

 

}
