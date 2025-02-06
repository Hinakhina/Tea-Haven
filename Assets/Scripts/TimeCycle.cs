using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CafeDayCycleManager : MonoBehaviour
{
    [SerializeField] GameObject blackPanel;

    private bool isShopOpen = false;
    private bool isWaitingForOrdersToComplete = false;

    public int totalCoins = 0; // Coins earned
    private List<CustomerOrder> activeOrders = new List<CustomerOrder>(); // Track active customer orders

    private CustomerSpawner customerSpawner; // Reference to CustomerSpawner

    void Start()
    {
        LoadProgress();
        StartNewDay();

        customerSpawner = FindObjectOfType<CustomerSpawner>();
        ClockTimeRun.OnMinuteChanged += CheckClosingConditions;
    }

    void OnDestroy()
    {
        ClockTimeRun.OnMinuteChanged -= CheckClosingConditions;
    }

    private void CheckClosingConditions()
    {
        if (isShopOpen)
        {
            if (ClockTimeRun.Hour >= 17 && !isWaitingForOrdersToComplete)
            {
                // Stop spawning new customers
                if (customerSpawner != null)
                {
                    customerSpawner.enabled = false;
                    Debug.Log("Customer spawning disabled.");
                }

                if (activeOrders.Count > 0)
                {
                    // Freeze time until all orders are completed
                    isWaitingForOrdersToComplete = true;
                    Debug.Log("Waiting for all customer orders to be completed...");
                }
                else
                {
                    StartCoroutine(ClosingSequence());
                }
            }
        }
        else if (isWaitingForOrdersToComplete && activeOrders.Count == 0)
        {
            // All orders are completed, proceed to closing
            isWaitingForOrdersToComplete = false;
            StartCoroutine(ClosingSequence());
        }
    }

    public void AddCoins(int amount)
    {
        totalCoins += amount;
        Debug.Log("Coins: " + totalCoins);
    }

    public void NewOrderReceived(CustomerOrder order)
    {
        if (!activeOrders.Contains(order))
        {
            activeOrders.Add(order);
            Debug.Log("New customer order received.");
        }
    }

    public void OrderCompleted(CustomerOrder order)
    {
        if (activeOrders.Contains(order))
        {
            activeOrders.Remove(order);
            Debug.Log("Customer order completed.");
        }
    }

    private void StartNewDay()
    {
        ClockTimeRun.Hour = 8;
        ClockTimeRun.Minute = 0;
        isShopOpen = true;

        if (customerSpawner != null)
        {
            customerSpawner.enabled = true; // Re-enable customer spawning
            Debug.Log("Customer spawning enabled.");
        }

        Debug.Log("Cafe is now open! Coins: " + totalCoins);
    }

    private IEnumerator ClosingSequence()
    {
        isShopOpen = false;
        Debug.Log("Closing scene starts...");

        // Simulate closing scene duration
        yield return new WaitForSeconds(3f);
        blackPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        blackPanel.SetActive(false);

        SaveProgress();
        ProceedToNextDay();
    }

    private void SaveProgress()
    {
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save();
        Debug.Log("Progress saved with coins: " + totalCoins);
    }

    private void LoadProgress()
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0); // Default is 0 if no data exists
        Debug.Log("Progress loaded. Coins: " + totalCoins);
    }

    private void ProceedToNextDay()
    {
        Debug.Log("Proceeding to the next day...");
        StartNewDay();
    }
}
