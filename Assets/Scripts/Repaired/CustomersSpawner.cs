using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CustomersSpawner : MonoBehaviour
{
    public GameObject customerPrefab; // Prefab for the customer
    public Transform spawnPoint, orderPoint; // Spawn and order positions
    public Sprite[] customerSprites; // 5 different customer sprites

    private GameObject currentCustomer;
    [SerializeField] OrderManagers orderManagers;
    // [SerializeField] ClockTimeRun clock;
    // [SerializeField] ServeTea serveTea;
    public bool isCustomerPresent = false;

    // Events
    public delegate void CustomerArrivedHandler();
    public event CustomerArrivedHandler OnCustomerArrived;

    public delegate void CustomerLeftHandler();
    public event CustomerLeftHandler OnCustomerLeft;

    void Start()
    {
        StartCoroutine(SpawnCustomerRoutine());
    }

    IEnumerator SpawnCustomerRoutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => !isCustomerPresent); // Wait until the previous customer leaves

            if (ClockTimeRun.Hour >= 17) // 5:00 PM
            {
                Debug.Log("Shop closed, no more customers.");
                yield break; // Stop the coroutine
            }

            yield return new WaitForSeconds(Random.Range(3f, 5f)); // Delay before new customer

            SpawnCustomer();
        }
    }

    void SpawnCustomer()
    {
        isCustomerPresent = true;

        // Create customer object
        currentCustomer = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);

        // Assign a random sprite
        SpriteRenderer spriteRenderer = currentCustomer.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = customerSprites[Random.Range(0, customerSprites.Length)];

        // Move customer to order point
        AudioManagers.Instance.PlaySFX("door");
        StartCoroutine(MoveCustomer(currentCustomer, orderPoint.position, () =>
        {
            orderManagers.StartNewOrder(); // Generate order when customer arrives
            OnCustomerArrived?.Invoke(); // Trigger event for customer arrival
        }));
    }

    public void ServeTeaFeedBack()
    {
        if (!isCustomerPresent) return; // No customer to serve

        StartCoroutine(CustomerLeaves());
    }

    public IEnumerator CustomerLeaves()
    {
        UnityEngine.Debug.Log("CustomerLeaving");
        yield return new WaitForSeconds(2f); // Wait before leaving
        orderManagers.feedbackText.text = ""; // Clear feedback
        orderManagers.textbubble.SetActive(false);

        StartCoroutine(MoveCustomer(currentCustomer, spawnPoint.position, () =>
        {
            AudioManagers.Instance.PlaySFX("door");
            Destroy(currentCustomer);
            isCustomerPresent = false;
            OnCustomerLeft?.Invoke(); // Trigger event for customer leaving
        }));
    }

    IEnumerator MoveCustomer(GameObject customer, Vector2 target, System.Action onComplete)
    {
        float speed = 2f;
        while (Vector2.Distance(customer.transform.position, target) > 0.1f)
        {
            customer.transform.position = Vector2.MoveTowards(customer.transform.position, target, speed * Time.deltaTime);
            yield return null;
        }

        onComplete?.Invoke();
    }
}
