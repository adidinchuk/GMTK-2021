
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThrustController2D : MonoBehaviour
{
    public float maxMoveSpeed = 30;
    public float maxAngularVelocity = 200;
    public float moveAcceleration = 10;
    public float turnAcceleration = 3;


    private Rigidbody2D rb;
    private float currentSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public void Move(float verticalAxis, float horizontalAxis, float deltaTime)
    {
        // Calculate speed from input and moveAcceleration (transform.up is forward)
        Vector2 moveSpeed = transform.up * (verticalAxis * moveAcceleration);
        float turnSpeed = horizontalAxis * turnAcceleration;

        rb.AddForce(moveSpeed * deltaTime);
        rb.AddTorque(turnSpeed * deltaTime);

        // Max Speed
        if (rb.velocity.magnitude > maxMoveSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxMoveSpeed;
        }

        if (rb.angularVelocity > maxAngularVelocity || rb.angularVelocity < -maxAngularVelocity)
        {
            rb.angularVelocity = Mathf.Sign(rb.angularVelocity) * maxAngularVelocity;
        }

        currentSpeed = rb.velocity.magnitude;

    }


}
