using System.Collections.Generic;
using UnityEngine;

public class CustomerOrder : MonoBehaviour
{
    [SerializeField] private List<string> orderIngredients = new List<string>();

    private Dictionary<string, List<string>> teaRecipes = new Dictionary<string, List<string>>()
    {
        { "Green Tea", new List<string> { "Tea", "Matcha Powder" } },
        { "Black Tea", new List<string> { "Tea" } },
        { "Oolong Tea", new List<string> { "Tea", "Sugar" } },
        { "Chamomile Tea", new List<string> { "Tea", "Milk" } }
    };

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
        int randomIndex = UnityEngine.Random.Range(0, teaOrders.Length);
        currentOrder = teaOrders[randomIndex];
        orderIngredients = new List<string>(teaRecipes[currentOrder]);
        Debug.Log("Customer ordered: " + string.Join(", ", currentOrder));
        Debug.Log("Selected Tea: " + currentOrder);
        Debug.Log("Correct Ingredients: " + string.Join(", ", orderIngredients));

        if (gpuiManager != null)
        {
            gpuiManager.ShowTeaBrewingPanel(currentOrder, orderIngredients);
        }
    }

    private string GetRandomTeaOrder()
    {
        List<string> teaNames = new List<string>(teaRecipes.Keys);
        return teaNames[Random.Range(0, teaNames.Count)];
    }

    public List<string> GetOrderIngredients()
    {
        return orderIngredients;
    }
}
