using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CustomerSpawner : MonoBehaviour
{
    public static CustomerSpawner Instance { get; private set; }

    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int maxCustomers = 1;
    [SerializeField] private int currentCustomers = 0;
    [SerializeField] private float customerMoveSpeed = 2f;

    private GameObject activeCustomer;

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
            if (activeCustomer == null && customerPrefab != null)
            {
                activeCustomer = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
                activeCustomer.SetActive(false); // Hide it initially
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (currentCustomers == 0 && maxCustomers > 0 && ClockTimeRun.Hour < 17)
        {
            SpawnNewCustomer();
        }
        Debug.Log("Total customers at start: " + currentCustomers);
    }

    public void SpawnNewCustomer()
    {
        Debug.Log("SpawnNewCustomer() called!");
        if (ClockTimeRun.Hour >= 17)
        {
            Debug.Log("No customers after 17:00.");
            return;
        }
        if (currentCustomers >= maxCustomers)
        {
            Debug.Log("Max customers reached");
            return;
        }

        if (activeCustomer != null)
        {
            // Reuse existing customer
            activeCustomer.transform.position = spawnPoint.position;

            // Set movement speed
            CustomerMovement movement = activeCustomer.GetComponent<CustomerMovement>();
            if (movement != null)
            {
                movement.moveSpeed = customerMoveSpeed;
                movement.ResetPosition();
            }

            activeCustomer.SetActive(true);
            Debug.Log($"Reactivated customer: {activeCustomer.name}");
            currentCustomers++;
        }
        else
        {
            Debug.LogError("Active customer reference is null! This shouldn't happen.");
        }
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
        Debug.Log("CustomerLeft() called!");
        if (currentCustomers > 0) 
        {
            currentCustomers--;
        }
        Debug.Log($"Customer left. Current count: {currentCustomers}");
        if (activeCustomer != null)
        {
            activeCustomer.SetActive(false);
        }
        if (ClockTimeRun.Hour < 17)
        {
            StartCoroutine(SpawnNextCustomerAfterDelay());
        }
        // StartCoroutine(SpawnNextCustomerAfterDelay());
    }
    private IEnumerator SpawnNextCustomerAfterDelay()
    {
        Debug.Log("SpawnNextCustomerAfterDelay() started!");
        yield return new WaitForSeconds(1f);
        // Debug.Log("Spawning new customer in 1 second...");
        // SpawnNewCustomer();
        if (ClockTimeRun.Hour < 17)
        {
            Debug.Log("Spawning new customer...");
            SpawnNewCustomer();
        }
        else
        {
            Debug.Log("Time is past 17:00, no new customers will spawn.");
        }
    }

    public bool HasActiveCustomers()
    {
        return currentCustomers > 0;
    }
}
