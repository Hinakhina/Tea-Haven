using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TimeCycle : MonoBehaviour
{
    private CustomerSpawner customerSpawner;
    // private bool isWaitingForOrdersToComplete = false;
    public int currentHour = 8; // Start at 8:00 AM
    public int closingHour = 17; // Shop closes at 5:00 PM
    [SerializeField] GameObject blackPanel;
    void Start()
    {
        customerSpawner = FindObjectOfType<CustomerSpawner>(); // Assign instance
        if (customerSpawner == null)
        {
            Debug.LogError("CustomerSpawner not found in the scene!");
        }
    }
    void Update()
    {
        SimulateTime();
    }
    void SimulateTime()
    {
        // Assuming this is called every in-game minute
        currentHour = (currentHour + 1) % 24; // Increment time (example logic)
        if (currentHour >= closingHour)
        {
            CheckClosingConditions();
        }
    }
    void CheckClosingConditions()
    {
        if (CustomerSpawner.Instance.HasActiveCustomers())
        {
            // isWaitingForOrdersToComplete = true;
        }
        else
        {
            EndDay();
        }
    }
    void EndDay()
    {
        Debug.Log("Day ended. Saving progress...");
        SaveProgress();
        AdvanceToNextDay();
    }
    void SaveProgress()
    {
        SavingManager.Instance.CompleteDay();
        Debug.Log("Game progress saved.");
    }
    public IEnumerator AdvanceToNextDay()
    {
        yield return new WaitForSeconds(3f); // Initial delay
        blackPanel.SetActive(true); // Show transition effect
        yield return new WaitForSeconds(3f); // Wait while black panel is visible
        ResetGameState(); // Restart the day
        blackPanel.SetActive(false); // Remove transition effect
        Debug.Log("New day started!");
    }
    void ResetGameState()
    {
        ClockTimeRun.Hour = 8; // Reset time
        ClockTimeRun.Minute = 0;
        Debug.Log("Game state reset for new day.");
    }
}