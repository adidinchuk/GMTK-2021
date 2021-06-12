using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundDistanceAdjuster : MonoBehaviour
{
    private Transform main;
    public float distance;
    // Use this for initialization
    void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.transform.position = Vector2.MoveTowards(child.gameObject.transform.position, main.position, ((child.gameObject.transform.position - main.position).magnitude) * distance);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

}
