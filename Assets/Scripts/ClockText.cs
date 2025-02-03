using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;

    void Awake()
    {
        if (timeText == null)
        {
            timeText = GetComponent<TextMeshProUGUI>();
        }
    }

    void OnEnable()
    {
        ClockTimeRun.OnMinuteChanged += UpdateTime;
        ClockTimeRun.OnHourChanged += UpdateTime;
        UpdateTime(); // Initial update
    }

    void OnDisable()
    {
        ClockTimeRun.OnMinuteChanged -= UpdateTime;
        ClockTimeRun.OnHourChanged -= UpdateTime;
    }

    private void UpdateTime()
    {
        timeText.text = $"{ClockTimeRun.Hour:00}:{ClockTimeRun.Minute:00}";
    }
}