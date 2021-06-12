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

        if (layer == 0)
        {
            speed = -2f;
            offset = new Vector3(0, 0, 0);
        }
        else if (layer == 1)
        {
            speed = 0.28f;
            offset = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), 0);
        }
        else if (layer == 2)
        {
            speed = 0.55f;
            offset = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), 0);
        }
        else if (layer == 3)
        {
            speed = 0.98f;
            offset = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), 0);
        }
        else if (layer == 4)
        {
            speed = 0.14f;
            offset = new Vector3(0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
       // if (!rocket.isDead)
            transform.position = new Vector3(mainTransform.position.x * speed, mainTransform.position.y * speed,0) + offset;
    }
}
