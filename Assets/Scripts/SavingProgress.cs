using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CafeDayCycleManager : MonoBehaviour
{
    public float timeScale = 60f; // 1 real second = 1 in-game minute
    private float currentTime; // Time in minutes since 00:00
    private bool isShopOpen = false;
    private bool isWaitingForCustomersToLeave = false;

    public int totalCoins = 0; // Coins earned
    private int activeCustomers = 0; // Track active customers

    void Start()
    {
        LoadProgress();
        StartNewDay();
    }

    void Update()
    {
        if (isShopOpen == true){
            currentTime += Time.deltaTime * timeScale;

            if (currentTime >= 1020 && !isWaitingForCustomersToLeave){ // 17:00 in minutes
                if (activeCustomers > 0)
                {
                    // Freeze time until customers leave
                    isWaitingForCustomersToLeave = true;
                    Debug.Log("Waiting for customers to leave...");
                }
                else
                {
                    StartCoroutine(ClosingSequence());
                }
            }
        }
        else if (isWaitingForCustomersToLeave && activeCustomers == 0){
            // All customers have left, proceed to closing
            isWaitingForCustomersToLeave = false;
            StartCoroutine(ClosingSequence());
        }
    }

    public void AddCoins(int amount)
    {
        totalCoins += amount;
        Debug.Log("Coins: " + totalCoins);
    }

    public void CustomerEntered()
    {
        activeCustomers++;
        Debug.Log("Customer entered. Active customers: " + activeCustomers);
    }

    public void CustomerLeft()
    {
        activeCustomers = Mathf.Max(0, activeCustomers - 1);
        Debug.Log("Customer left. Active customers: " + activeCustomers);
    }

    private void StartNewDay()
    {
        currentTime = 480; // 8:00 AM in minutes
        isShopOpen = true;
        Debug.Log("Cafe is now open! Coins: " + totalCoins);
    }

    private IEnumerator ClosingSequence()
    {
        isShopOpen = false;
        Debug.Log("Closing scene starts...");

        //closing scene
        yield return new WaitForSeconds(3f);

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
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        Debug.Log("Progress loaded. Coins: " + totalCoins);
    }

    private void ProceedToNextDay()
    {
        Debug.Log("Proceeding to the next day...");
        StartNewDay();
    }
}
