using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundDebris : MonoBehaviour
{
    [SerializeField]
    private Transform main;
    private Rigidbody2D rb;
    private float maxDistance = 30;
    private float scale;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = (Random.value-0.5f)*100;
        scale = (Random.value + 1f)/2;
        transform.localScale = transform.localScale * scale;
    }

    private void Update()
    {
        //transform.position = new Vector3(main.position.x * (scale + 2) / 6, main.position.y * (scale + 2) / 6, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xPos = transform.position.x;
        float yPos = transform.position.x;
        bool updatePos = false;
        if (xPos - main.position.x > maxDistance)
        {
            xPos = main.position.x - maxDistance+2;
            updatePos = true;
        }
        else if (transform.position.x - main.position.x < -maxDistance)
        {
            xPos = main.position.x + maxDistance-2;
            updatePos = true;
        }
        if (yPos - main.position.y > maxDistance)
        {
            yPos = main.position.y - maxDistance+2;
            updatePos = true;
        }
        else if (transform.position.y - main.position.y < -maxDistance)
        {
            yPos = main.position.y + maxDistance-2;
            updatePos = true;
        }
        if (updatePos)
        {
            transform.position = new Vector3(xPos, yPos, 0);
        }
        
    }
}
