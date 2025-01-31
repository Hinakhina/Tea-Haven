using UnityEngine;

public class CustomerOrder : MonoBehaviour
{
    private string[] teaOrders = { "Green Tea", "Black Tea", "Oolong Tea", "Chamomile Tea" };
    private string currentOrder;
    private GameplayUIManager gpuiManager;
    private CustomerMovement customerMovement;

    private void Start()
    {
        gpuiManager = FindObjectOfType<GameplayUIManager>();
        customerMovement = GetComponent<CustomerMovement>();

        if (customerMovement != null)
        {
            customerMovement.OnCustomerArrived += AssignRandomOrder;
        }
    }

    private void AssignRandomOrder()
    {
        currentOrder = teaOrders[Random.Range(0, teaOrders.Length)];
        Debug.Log("Customer ordered: " + currentOrder);

        if (gpuiManager != null)
        {
            gpuiManager.ShowTeaBrewingPanel(currentOrder);
        }
    }

    public string GetOrder()
    {
        return currentOrder;
    }
}
