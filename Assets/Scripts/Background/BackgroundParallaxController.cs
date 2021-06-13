using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallaxController : MonoBehaviour
{

    public int layer;
    public Transform mainTransform;
    private float speed;
    private Vector3 offset;
    // Use this for initialization
    void Start()
    {

        if (layer == 1)
        {
            speed = 0.4f;

        }
        else if (layer == 2)
        {
            speed = 0.5f;

        }
        else if (layer == 3)
        {
            speed = 0.6f;

        }
        else if (layer == 4)
        {
            speed = 0.98f;

        }       
    }

    public void adjustSpeed()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
       // if (!rocket.isDead)
            transform.position = new Vector3(mainTransform.position.x * speed, mainTransform.position.y * speed,0);
    }
}
