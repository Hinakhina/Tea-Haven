using UnityEngine;
using System.Collections;

public class CustomerMovement : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 orderPosition;
    public float moveSpeed = 2f;

    public delegate void CustomerArrivedHandler();
    public event CustomerArrivedHandler OnCustomerArrived;

    private void OnEnable()
    {
        Debug.Log($"Customer {gameObject.name} enabled with speed: {moveSpeed}");
        ResetPosition();
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        Debug.Log($"Customer {gameObject.name} reset position with speed: {moveSpeed}");
        StartCoroutine(MoveToPosition(orderPosition));
    }

    private IEnumerator MoveToPosition(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }

        OnCustomerArrived?.Invoke();
    }
}