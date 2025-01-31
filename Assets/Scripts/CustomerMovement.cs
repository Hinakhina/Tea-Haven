using UnityEngine;
using System.Collections;

public class CustomerMovement : MonoBehaviour
{
    public Vector3 startPosition;  // Off-screen starting point
    public Vector3 orderPosition;  // Where they stop to order
    public float moveSpeed = 2f;   // Speed of movement

    public delegate void CustomerArrivedHandler();
    public event CustomerArrivedHandler OnCustomerArrived; // Event for when the customer stops

    private void Start()
    {
        transform.position = startPosition;
        StartCoroutine(MoveToPosition(orderPosition));
    }

    private IEnumerator MoveToPosition(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Customer has arrived, trigger the event
        OnCustomerArrived?.Invoke();
    }
}
