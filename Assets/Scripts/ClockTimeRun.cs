using UnityEngine;
using System;

public class ClockTimeRun : MonoBehaviour
{
    public static event Action OnMinuteChanged;
    public static event Action OnHourChanged;
    
    public static double Minute { get; private set; }
    public static double Hour { get; private set; }
    
    [SerializeField] private float minuteIrl = 0.5f;
    private float timer;

    void Start()
    {
        ResetTime();
    }

    void Update()
    {
        if (Hour < 17)
        {
            timer -= Time.deltaTime;
            while (timer <= 0)
            {
                Minute += 5;
                OnMinuteChanged?.Invoke();

                if (Minute >= 60)
                {
                    Hour++;
                    Minute = 0;
                    OnHourChanged?.Invoke();
                }

                timer = minuteIrl;
            }
        }
    }

    public void ResetTime(double startHour = 8)
    {
        Minute = 0;
        Hour = startHour;
        timer = minuteIrl;
        OnMinuteChanged?.Invoke();
        OnHourChanged?.Invoke();
    }
}