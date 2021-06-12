using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ThrustController2D))]
public class ShipPartMovement : MonoBehaviour
{
    private ThrustController2D controller;

    private bool thrustersAttached;

    // Four (4) thrusters, to enable animation
    private ShipPartThruster thrusterTopLeft;
    private ShipPartThruster thrusterTopRight;
    private ShipPartThruster thrusterBottomLeft;
    private ShipPartThruster thrusterBottomRight;


    // Force of Acceleration
    private float horizontalAxis = 0f;
    private float verticalAxis = 0f;


    // Track selection
    private bool mouseOver = false;

    private void Awake()
    {
        controller = GetComponent<ThrustController2D>();

        thrusterTopLeft = transform.Find("ThrusterTopLeft").GetComponent<ShipPartThruster>();
        thrusterTopRight = transform.Find("ThrusterTopRight").GetComponent<ShipPartThruster>();
        thrusterBottomLeft = transform.Find("ThrusterBottomLeft").GetComponent<ShipPartThruster>();
        thrusterBottomRight = transform.Find("ThrusterBottomRight").GetComponent<ShipPartThruster>();

        DetachThrusters();
    }


    void OnMouseEnter()
    {
        mouseOver = true;
    }

    void OnMouseExit()
    {
        mouseOver = false;
    }


    // Update is called once per frame
    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (mouseOver)
            {
                AttachThrusters();
            }
            else
            {
                DetachThrusters();
            }
        }


        if (!thrustersAttached) return;

        verticalAxis = Input.GetAxis("Vertical");
        horizontalAxis = -Input.GetAxis("Horizontal");

        // Start animations
        if (horizontalAxis < 0)
        {
            thrusterTopLeft.Thrust();
            thrusterBottomRight.Thrust();
        }

        if (horizontalAxis > 0)
        {
            thrusterTopRight.Thrust();
            thrusterBottomLeft.Thrust();

        }

        if (verticalAxis > 0)
        {
            thrusterBottomLeft.Thrust();
            thrusterBottomRight.Thrust();
        }


        if (verticalAxis < 0)
        {
            thrusterTopRight.Thrust();
            thrusterTopLeft.Thrust();
        }
    }



    private void FixedUpdate()
    {
        controller.Move(verticalAxis, horizontalAxis, Time.deltaTime);
    }

    public void AttachThrusters()
    {
        thrustersAttached = true;
        thrusterTopLeft.gameObject.SetActive(true);
        thrusterTopRight.gameObject.SetActive(true);
        thrusterBottomLeft.gameObject.SetActive(true);
        thrusterBottomRight.gameObject.SetActive(true);
    }

    public void DetachThrusters()
    {
        thrustersAttached = false;
        thrusterTopLeft.gameObject.SetActive(false);
        thrusterTopRight.gameObject.SetActive(false);
        thrusterBottomLeft.gameObject.SetActive(false);
        thrusterBottomRight.gameObject.SetActive(false);
    }
}
