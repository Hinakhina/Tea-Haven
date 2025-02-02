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
    [SerializeField] private CustomerMovement customerMovement;

    private string[] teaOrders = { "Chrysantemum Tea", "Green Tea", "Oolong Tea", "Lavender Tea" };
    private string currentOrder;

    private void Start()
    {
        gpuiManager = FindObjectOfType<GameplayUIManager>();
        customerMovement = GetComponent<CustomerMovement>();
        customerSpawner = FindObjectOfType<CustomerSpawner>();

        if (customerMovement != null)
        {
            customerMovement.OnCustomerArrived += DisplayOrder;
        }
    }

    public void AssignOrder((string teaName, List<string> ingredients) order)
    {
        currentOrder = order.teaName;
        orderIngredients = new List<string>(order.ingredients);

        if (orderText != null)
        {
            orderText.text = "Order: " + currentOrder;
        }
        else
        {
            Debug.LogError("OrderText is not assigned in the inspector!");
        }

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
        Debug.Log("Customer ordered: " + currentOrder);
        Debug.Log("Correct Ingredients: " + string.Join(", ", orderIngredients));
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
        Destroy(gameObject);
        CustomerSpawner.Instance.CustomerLeft();
    }
}
