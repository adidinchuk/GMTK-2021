using System;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public float radius = 10f;
    public event EventHandler OnGoalReached;


    private void Awake()
    {
        var angle = UnityEngine.Random.Range(0, 1f) * Mathf.PI * 2;

        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        transform.position = new Vector3(x,y);
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        ShipPart otherShipPart = col.gameObject.GetComponent<ShipPart>();
        if (otherShipPart == null) return;

        // if col is our ship
        OnGoalReached?.Invoke(this, EventArgs.Empty);
    }

}
