using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainShip : MonoBehaviour
{
    int totalScore = 0;
    private void Update()
    {
        //if (Input.GetKeyDown("space"))
        //{
        //    int score = GetScore();
        //    print("Score:  " + score);
        //}
    }

    public int GetScore()
    {
        ShipPart mainShipPart = gameObject.GetComponent<ShipPart>();
        return BFSearch.Score(mainShipPart, mainShipPart) + totalScore;

    }

    public void Jettison()
    {
        ShipPart mainShipPart = gameObject.GetComponent<ShipPart>();
        HashSet<ShipPart> reached = BFSearch.Search(mainShipPart, mainShipPart);
        
        foreach (ShipPart shipPart in reached)
        {
            if (shipPart.gameObject.GetInstanceID() != this.GetInstanceID())
            {
                totalScore += shipPart.GetScore(shipPart);
                Destroy(shipPart);
            }
        }

    }
}
