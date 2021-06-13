using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainShip : MonoBehaviour
{
    private int totalWeightDelivered = 0;
    private ShipPart mainShipPart;
    
    private void Awake()
    {
        mainShipPart = gameObject.GetComponent<ShipPart>();
    }
    private void Update()
    {
        //if (Input.GetKeyDown("space"))
        //{
        //    int score = GetScore();
        //    print("Score:  " + score);
        //}


    }

    public int GetTotalWeightDelivered()
    {
        return totalWeightDelivered;
    }

    public int GetCarriedWeight()
    {
        int currentWeight = BFSearch.SumWeight(mainShipPart, mainShipPart);
        int mainShipWeight = mainShipPart.GetWeight(mainShipPart);
     
        return currentWeight - mainShipWeight;
    }

    public void Jettison()
    {
        ShipPart mainShipPart = gameObject.GetComponent<ShipPart>();
        HashSet<ShipPart> reached = BFSearch.Search(mainShipPart, mainShipPart);
        
        foreach (ShipPart shipPart in reached)
        {
            if (shipPart != mainShipPart)
            {
                totalWeightDelivered += shipPart.GetWeight(shipPart);
                Destroy(shipPart.gameObject);
            }
        }

        FixedJoint2D[] joints = mainShipPart.gameObject.GetComponents<FixedJoint2D>();
        for (int i = 0; i < joints.Length; i++)
        {
            Destroy(joints[i]);
        }

    }
}
