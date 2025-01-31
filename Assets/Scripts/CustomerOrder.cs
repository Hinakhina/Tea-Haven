using UnityEngine;

public class CustomerOrder : MonoBehaviour
{
    private string[] teaOrders = { "Green Tea", "Black Tea", "Oolong Tea", "Chamomile Tea" };
    private string currentOrder;
    private UIManager uiManager;
    private CustomerMovement customerMovement;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>(); // Get UIManager reference
        customerMovement = GetComponent<CustomerMovement>(); // Get CustomerMovement reference

        if (customerMovement != null)
        {
            customerMovement.OnCustomerArrived += AssignRandomOrder; // Wait until arrival to order
        }
    }

    private void AssignRandomOrder()
    {
        currentOrder = teaOrders[Random.Range(0, teaOrders.Length)];
        Debug.Log("Customer ordered: " + currentOrder);

        if (uiManager != null)
        {
            uiManager.UpdateStatusText("Order: " + currentOrder);
        }
    }

    public string GetOrder()
    {
        return currentOrder;
    }
}
