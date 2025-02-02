using UnityEngine;
using System.Collections.Generic;

public class CustomerSpawner : MonoBehaviour
{
    public static CustomerSpawner Instance { get; private set; }

    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int maxCustomers = 1;
    private int currentCustomers = 0;

    public Dictionary<string, List<string>> teaRecipes = new Dictionary<string, List<string>>()
    {
        { "Chrysantemum Tea", new List<string> { "Chrysantemum Tea Leaves", "Hot Water" } },
        { "Green Tea", new List<string> { "Matcha Powder", "Hot Water" } },
        { "Oolong Tea", new List<string> { "Oolong Tea Leaves", "Hot Water" } },
        { "Lavender Tea", new List<string> { "Lavender Tea Leaves", "Hot Water", "Sugar" } }
    };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        int existingCustomers = FindObjectsOfType<CustomerOrder>().Length;
        Debug.Log("Total customers at start: " + existingCustomers);

        if (existingCustomers == 0 && maxCustomers > 0)
        {
            SpawnNewCustomer();
        }
    }

    public void SpawnNewCustomer()
    {
        if (currentCustomers >= maxCustomers)
        {
            Debug.Log("Max customers reached");
            return;
        }

        if (customerPrefab == null)
        {
            Debug.LogError("Customer prefab is missing!");
            return;
        }

        GameObject newCustomer = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
        CustomerOrder customerOrder = newCustomer.GetComponent<CustomerOrder>();
        CustomerAppearance appearance = newCustomer.GetComponent<CustomerAppearance>();

        if (customerOrder != null)
        {
            var order = GetRandomOrder();
            customerOrder.AssignOrder(order);
        }

        if (appearance != null)
        {
            Debug.Log("Customer appearance script found. Assigning sprite...");
        }
        else
        {
            Debug.LogError("CustomerAppearance script is missing from the prefab!");
        }
        newCustomer.SetActive(true);
        Debug.Log("Spawned new customer: " + newCustomer.name);

        currentCustomers++;
        Debug.Log("Spawned new customer. Current count: " + currentCustomers);
    }

    public (string, List<string>) GetRandomOrder()
    {
        List<string> teaNames = new List<string>(teaRecipes.Keys);
        string randomTea = teaNames[Random.Range(0, teaNames.Count)];
        var ingredients = teaRecipes[randomTea];
        Debug.Log($"GetRandomOrder: Selected {randomTea} with ingredients: {string.Join(", ", ingredients)}");
        return (randomTea, ingredients);
    }

    public void CustomerLeft()
    {
        if (currentCustomers > 0) 
        {
            currentCustomers--;
            Debug.Log("Customer left. Current count: " + currentCustomers);
        }
        if (currentCustomers < maxCustomers)
        {
            SpawnNewCustomer();
        }
        else
        {
            Debug.LogWarning("Tried to reduce customers below zero!");
        }
    }
    
}
