using System.Collections.Generic;
using UnityEngine;

public class CustomerOrder : MonoBehaviour
{
    private string[] teaOrders = { "Green Tea", "Black Tea", "Oolong Tea", "Chamomile Tea" };
    private List<string> currentOrder = new List<string>();
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
        currentOrder.Clear();
        currentOrder.Add(teaOrders[Random.Range(0, teaOrders.Length)]);
        Debug.Log("Customer ordered: " + string.Join(", ", currentOrder));

        if (gpuiManager != null)
        {
            gpuiManager.ShowTeaBrewingPanel(new List<string>(currentOrder));
        }
    }

    public List<string> GetOrder()
    {
        return new List<string>(currentOrder);
    }
}
