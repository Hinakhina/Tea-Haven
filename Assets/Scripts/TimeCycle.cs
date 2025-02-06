using UnityEngine;
using System.Collections;

public class TimeCycle : MonoBehaviour
{
    [SerializeField] private GameObject blackPanel;
    
    private void Start()
    {
        ClockTimeRun clockTime = GetComponent<ClockTimeRun>();
        if (clockTime == null)
            clockTime = gameObject.AddComponent<ClockTimeRun>();

        ClockTimeRun.OnHourChanged += CheckDayEnd;
    }

    private void CheckDayEnd()
    {
        if (ClockTimeRun.Hour >= 17 && !CustomerSpawner.Instance.HasActiveCustomers())
        {
            StartCoroutine(EndDaySequence());
        }
    }

    private IEnumerator EndDaySequence()
    {
        CustomerSpawner.Instance.StopSpawning();

        while (CustomerSpawner.Instance.HasActiveCustomers())
        {
            yield return new WaitForSeconds(1f);
        }

        blackPanel.SetActive(true);
        yield return new WaitForSeconds(3f);

        SavingManager.Instance.CompleteDay();
        GetComponent<ClockTimeRun>().ResetTime();

        blackPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        ClockTimeRun.OnHourChanged -= CheckDayEnd;
    }
}