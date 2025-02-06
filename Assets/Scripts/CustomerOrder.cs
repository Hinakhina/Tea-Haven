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
    private string currentOrder;
    private bool orderDisplayed = false;

    private void OnEnable()
    {
        InitializeComponents();
        if (ClockTimeRun.Hour < 17)
        {
            RequestNewOrder();
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

        string orderDisplay = $"Order: {currentOrder}\nIngredients: {string.Join(", ", orderIngredients)}";

        if (orderText != null)
        {
            orderText.text = orderDisplay;
            Debug.Log($"Assigned Order: {orderDisplay}");
        }

        if (gpuiManager != null)
        {
            gpuiManager.ShowTeaBrewingPanel(currentOrder, orderIngredients);
        }
    }

    private void DisplayOrder()
    {
        if (!orderDisplayed)
        {
            Debug.Log($"Customer has arrived and ordered: {currentOrder}\nIngredients: {string.Join(", ", orderIngredients)}");
            orderDisplayed = true;
        }
    }

    public List<string> GetOrderIngredients()
    {
        return new List<string>(orderIngredients);
    }

    public void ReceiveTea(bool isCorrect)
    {
        if (isCorrect)
        {
            GameplayUIManager.Instance.ShowSuccessMessage("Thank you! This is exactly what I wanted!");
        }
        else
        {
            GameplayUIManager.Instance.ShowErrorMessage("Sorry, this isn't what I ordered...");
        }
        StartCoroutine(LeaveCustomer());
    }

    private IEnumerator LeaveCustomer()
    {
        yield return new WaitForSeconds(leaveDelay);
        GameplayUIManager.Instance.removeMessage("");
        orderDisplayed = false;

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