using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class TimeCycle : MonoBehaviour
{
    private CustomerSpawner customerSpawner;
    // private bool isWaitingForOrdersToComplete = false;
    public int currentHour = 8; // Start at 8:00 AM
    public int closingHour = 17; // Shop closes at 5:00 PM
    [SerializeField] GameObject blackPanel;
    private ClockTimeRun ClockTimeRun;

    private bool isDayEnding = false;

    void Start()
    {
        customerSpawner = FindObjectOfType<CustomerSpawner>(); // Assign instance
        if (customerSpawner == null)
        {
            Debug.LogError("CustomerSpawner not found in the scene!");
        }
    }
    void update()
    {
        if(ClockTimeRun.Hour >= 17 && isDayEnding == true)
        {
            CheckClosingConditions();
        }

    }
    public void CheckClosingConditions()
    {
        Debug.Log("Check Closing Condition...");
        if (CustomerSpawner.Instance.HasActiveCustomers()) 
        {
            // isWaitingForOrdersToComplete = true;
        }
        else
        {
            Debug.Log("Ending Day...");
            EndDay();
        }
    }
    void EndDay()
    {
        if(isDayEnding == true)
        {
            return;
        }
        isDayEnding = true;
        Debug.Log("Day ended. Saving progress...");
        SaveProgress();
    }
    void SaveProgress()
    {
        SavingManager.Instance.AddDays();
        SavingManager.Instance.CompleteDay();
        Debug.Log("Game progress saved.");
        StartCoroutine(AdvanceToNextDay());
    }
    public IEnumerator AdvanceToNextDay()
    {
        Debug.Log("Next Day in 3...2...1..");
        yield return new WaitForSeconds(3f); // Initial delay
        blackPanel.SetActive(true); // Show transition effect
        yield return new WaitForSeconds(3f); // Wait while black panel is visible
        
        // ResetGameState(); // Restart the day
        // blackPanel.SetActive(false); // Remove transition effect
        
        Debug.Log("New day started!");
        ContinueGame();
    }
    void ResetGameState()
    {
        ClockTimeRun.Hour = 8; // Reset time
        ClockTimeRun.Minute = 0;
        Debug.Log("Game state reset for new day.");
    }

    public void ContinueGame()
    {
        AudioManagers.Instance.PlaySFX("dink");
        Debug.Log("Continuing the game...");
        SceneManager.LoadScene("GamePlay");
    }
}