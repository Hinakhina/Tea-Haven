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
    public string currentOrder;

    private void OnEnable()
    {
        InitializeComponents();
        if (ClockTimeRun.Hour < 17)
        {
            RequestNewOrder();
        }
        else
        {
            Debug.Log("No new orders after 17:00.");

        }
    }

    private void InitializeComponents()
    {
        if (gpuiManager == null) gpuiManager = FindObjectOfType<GameplayUIManager>();
        if (customerMovement == null) customerMovement = GetComponent<CustomerMovement>();
        if (customerSpawner == null) customerSpawner = FindObjectOfType<CustomerSpawner>();
        if (orderText == null) orderText = GetComponentInChildren<TMP_Text>();

        if (customerMovement != null)
        {
            customerMovement.OnCustomerArrived += DisplayOrder;
        }
    }
    private void RequestNewOrder()
    {
        if (customerSpawner != null)
        {
            var order = customerSpawner.GetRandomOrder();
            AssignOrder(order);
        }
    }

    public void AssignOrder((string teaName, List<string> ingredients) order)
    {
        currentOrder = order.teaName;
        orderIngredients = new List<string>(order.ingredients);

        if (orderText != null)
        {
            orderText.text = "Order: " + currentOrder;
            Debug.Log($"Assigned Order: {currentOrder}");
        }

        if (gpuiManager != null)
        {
            gpuiManager.ShowTeaBrewingPanel(currentOrder, orderIngredients);
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
            SavingManager.Instance.AddCoins(10);
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

        // Clean up
        if (orderText != null)
        {
            orderText.text = "";
        }
        orderIngredients.Clear();
        currentOrder = "";

        CustomerSpawner.Instance.CustomerLeft();
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (customerMovement != null)
        {
            customerMovement.OnCustomerArrived -= DisplayOrder;
        }
    }
}