using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class CustomerOrder : MonoBehaviour
{
    [SerializeField] private List<string> orderIngredients = new List<string>();
    [SerializeField] private TMP_Text orderText;
    [SerializeField] private CustomerSpawner customerSpawner;
    [SerializeField] private float leaveDelay = 2f;
    [SerializeField] private GameplayUIManager gpuiManager;

    private CustomerMovement customerMovement;
    private string[] teaOrders = { "Chrysantemum Tea", "Green Tea", "Oolong Tea", "Lavender Tea" };
    private string currentOrder;

    private void Start()
    {
        gpuiManager = FindObjectOfType<GameplayUIManager>();
        customerMovement = GetComponent<CustomerMovement>();
        customerSpawner = FindObjectOfType<CustomerSpawner>();

        if (orderText == null)
        {
            orderText = GetComponentInChildren<TMP_Text>();
            if (orderText == null)
            {
                Debug.LogError("Could not find OrderText in children!");
            }
        }

        if (customerMovement != null)
        {
            customerMovement.OnCustomerArrived += DisplayOrder;
        }

        if (customerSpawner != null)
        {
            var order = customerSpawner.GetRandomOrder();
            AssignOrder(order);
        }
    }

    public void AssignOrder((string teaName, List<string> ingredients) order)
    {

        if (orderText == null)
        {
            orderText = GetComponentInChildren<TMP_Text>(); // Auto-find order text
            if (orderText == null)
            {
                Debug.LogError("OrderText is missing in prefab!");
                return;
            }
        }

        currentOrder = order.teaName;
        orderIngredients = new List<string>(order.ingredients);
        orderText.text = "Order: " + currentOrder;
        Debug.Log($"Assigned Order: {currentOrder}");

        if (gpuiManager != null)
        {
            gpuiManager.ShowTeaBrewingPanel(currentOrder, orderIngredients);
        }
        else
        {
            Debug.LogError("gpuiManager is null in CustomerOrder!");
        }
    }

    private void DisplayOrder()
    {
        Debug.Log("Customer has arrived and ordered: " + currentOrder);
    }

    public List<string> GetOrderIngredients()
    {
        return orderIngredients;
    }

    public void ReceiveTea(bool isCorrect)
    {
        if (isCorrect)
        {
            GameplayUIManager.Instance.ShowSuccessMessage("Customer: Thank you!");
        }
        else
        {
            GameplayUIManager.Instance.ShowErrorMessage("Customer: This is wrong!");
        }
        StartCoroutine(LeaveCustomer());
        // rn customer leaves no matter what tea u brewed
    }

    private IEnumerator LeaveCustomer()
    {
        yield return new WaitForSeconds(leaveDelay);
        Debug.Log("Customer leaving...");
        GameplayUIManager.Instance.removeMessage("");

        Destroy(gameObject);
        CustomerSpawner.Instance.CustomerLeft();
    }
}
