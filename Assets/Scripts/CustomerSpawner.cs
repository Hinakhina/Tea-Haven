using UnityEngine;
using System.Collections.Generic;

public class CustomerSpawner : MonoBehaviour
{
    public static CustomerSpawner Instance { get; private set; }

    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int maxCustomers = 5;
    private int currentCustomers = 0;

    private Dictionary<string, List<string>> teaRecipes = new Dictionary<string, List<string>>()
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
        SpawnNewCustomer();
    }

    public void SpawnNewCustomer()
    {
        if (currentCustomers >= maxCustomers)
        {
            Debug.Log("Max customers reached!");
            return;
        }

        GameObject newCustomer = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
        CustomerOrder customerOrder = newCustomer.GetComponent<CustomerOrder>();

        if (customerOrder != null)
        {
            customerOrder.AssignOrder(GetRandomOrder());
        }

        currentCustomers++;
    }

    private (string, List<string>) GetRandomOrder()
    {
        List<string> teaNames = new List<string>(teaRecipes.Keys);
        string randomTea = teaNames[Random.Range(0, teaNames.Count)];
        return (randomTea, teaRecipes[randomTea]);
    }

    public void CustomerLeft()
    {
        currentCustomers--;
        SpawnNewCustomer();
    }
}
