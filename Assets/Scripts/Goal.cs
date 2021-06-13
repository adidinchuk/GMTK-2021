using System;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private int targetScore;

    public event EventHandler OnGoalReached;
    private bool goalReached = false;


    // TODO: Add a visual for the current goal
    public static Goal Create(Vector3 position, float radius, int targetScore) {
        var angle = UnityEngine.Random.Range(0, 1f) * Mathf.PI * 2;

        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        Vector3 spawnPosition = new Vector3(x + position.x, y + position.y);

        Transform pfGoal = Resources.Load<Transform>("pfGoal");
        Transform goalTransform = Instantiate(pfGoal, spawnPosition, Quaternion.identity);

        Goal goal = goalTransform.GetComponent<Goal>();
        goal.SetTargetScore(targetScore);

        return goal;
    }

    public void SetTargetScore(int targetScore)
    {
        this.targetScore = targetScore;
    }


    

    void OnTriggerEnter2D(Collider2D col)
    {
        MainShip otherShipPart = col.gameObject.GetComponent<MainShip>();
        if (otherShipPart == null || goalReached) return;

        // if col is our ship
        if (otherShipPart.GetScore() > targetScore) { 
            OnGoalReached?.Invoke(this, EventArgs.Empty);
            goalReached = true;
        } else {
            // Flash the text of the goal
            

        }
    }

}
