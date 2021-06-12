using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPartThruster : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private Color originalColor;


    private bool thrustThisUpdate = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {


        if (!thrustThisUpdate)
        {
            spriteRenderer.color = originalColor;
        }

        thrustThisUpdate = false;


    }


    public void Thrust()
    {
        spriteRenderer.color = Color.red;
        thrustThisUpdate = true;
    }
}
