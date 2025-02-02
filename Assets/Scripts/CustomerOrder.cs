using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class CustomerOrder : MonoBehaviour
{
    [SerializeField] private List<string> orderIngredients = new List<string>();
    [SerializeField] private TMP_Text orderText;
    [SerializeField] private float leaveDelay = 2f;

    private string[] teaOrders = { "Chrysantemum Tea", "Green Tea", "Oolong Tea", "Lavender Tea" };
    private string currentOrder;
    private GameplayUIManager gpuiManager;
    private CustomerMovement customerMovement;

    private void Start()
    {
        gpuiManager = FindObjectOfType<GameplayUIManager>();
        customerMovement = GetComponent<CustomerMovement>();

        if (customerMovement != null)
        {
            customerMovement.OnCustomerArrived += DisplayOrder;
        }
    }

    public void AssignOrder((string teaName, List<string> ingredients) order)
    {
        currentOrder = order.teaName;
        orderIngredients = new List<string>(order.ingredients);

        if (gpuiManager != null)
        {
            gpuiManager.ShowTeaBrewingPanel(currentOrder, orderIngredients);
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
            StartCoroutine(LeaveCustomer());
        }
        else
        {
            GameplayUIManager.Instance.ShowErrorMessage("Customer: This is wrong!");
            // rn customer doesnt leave after getting wrong order. kalo mau bikin dia leave tambahin line of code disni
            StartCoroutine(LeaveCustomer());
        }
    }

    private IEnumerator LeaveCustomer()
    {
        yield return new WaitForSeconds(leaveDelay);
        Destroy(gameObject);
        CustomerSpawner.Instance.SpawnNewCustomer(); // spawn new customer
    }
}
