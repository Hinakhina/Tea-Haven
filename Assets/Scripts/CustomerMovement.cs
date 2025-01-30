using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    public Transform stopPosition; // The point where the customer stops
    public float speed = 2f; // Movement speed

    private bool isMoving = true;

    void Update()
    {
        if (isMoving)
        {
            // Move towards the stop position
            transform.position = Vector2.MoveTowards(transform.position, stopPosition.position, speed * Time.deltaTime);

            // Stop moving when reaching the stop position
            if (Vector2.Distance(transform.position, stopPosition.position) < 0.1f)
            {
                isMoving = false;
            }
        }
    }
}
