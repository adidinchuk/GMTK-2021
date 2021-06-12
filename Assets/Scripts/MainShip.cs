using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainShip : MonoBehaviour
{

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
        return BFSearch.Search(mainShipPart, mainShipPart);

    }
}
