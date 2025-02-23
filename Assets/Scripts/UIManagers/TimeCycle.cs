using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TimeCycle : MonoBehaviour
{
    private CustomersSpawner customersSpawner;
    public int currentHour = 8; // Start at 8:00 AM
    public int closingHour = 17; // Shop closes at 5:00 PM
    [SerializeField] private GameObject blackPanel;
    private ClockTimeRun ClockTimeRun;

    private bool isDayEnding = false;

    void Start()
    {
        customersSpawner = FindObjectOfType<CustomersSpawner>();
        ClockTimeRun = FindObjectOfType<ClockTimeRun>();
        if (customersSpawner == null)
        {
            Debug.LogError("CustomerSpawner not found in the scene!");
        }
        else
        {
            customersSpawner.OnCustomerLeft += CheckClosingConditions; // Subscribe to event
        }
    }

    void Update()
    {
        if (currentHour >= closingHour && !isDayEnding) 
        {
            CheckClosingConditions();
        }
    }

    public void CheckClosingConditions()
    {
        if (isDayEnding) return; // Prevent multiple calls

        // Ensure the time is at least 17:00 before ending the day
        if (ClockTimeRun.Hour >= closingHour && !customersSpawner.isCustomerPresent) 
        {
            EndDay();
        }
    }

    void EndDay()
    {
        if (isDayEnding) return; 
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
        yield return new WaitForSeconds(3f);
        AudioManagers.Instance.PlaySFX("closing");
        blackPanel.SetActive(true);
        yield return new WaitForSeconds(3f);

        ResetGameState();
        AudioManagers.Instance.PlaySFX("opening");
        blackPanel.SetActive(false);

        Debug.Log("New day started!");
        ContinueGame();
    }

    void ResetGameState()
    {
        currentHour = 8;
        ClockTimeRun.Hour = 8;
        ClockTimeRun.Minute = 0;
        isDayEnding = false;
        Debug.Log("Game state reset for new day.");
    }

    public void ContinueGame()
    {
        AudioManagers.Instance.PlaySFX("dink");
        Debug.Log("Continuing the game...");
        SceneManager.LoadScene("GamePlay");
    }

    void OnDestroy()
    {
        if (customersSpawner != null)
        {
            customersSpawner.OnCustomerLeft -= CheckClosingConditions; // Unsubscribe when destroyed
        }
    }
}
