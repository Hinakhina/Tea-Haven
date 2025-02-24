using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ClockTimeRun : MonoBehaviour
{
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;
    public static double Minute {get; set;}
    public static double Hour {get; set;}
    private float minuteIrl = 0.5f; //2s irl = 1min in-game
    private float timer;
    public TimeCycle TimeCycle;
    // Start is called before the first frame update
    void Start()
    {
        Minute = 0;
        Hour = 8;
        timer = minuteIrl;
        OnMinuteChanged?.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        if (Hour < 17) // Prevent time from increasing after 17:00
        {
            timer -= Time.deltaTime;
            while (timer <= 0)
            {
                Minute += 0.7;
                // Minute += 5;
                OnMinuteChanged?.Invoke();
                if (Minute >= 60)
                {
                    Hour += 1;
                    Minute = 0;
                    OnHourChanged?.Invoke();
                }
                timer = minuteIrl;
            }
        }
        // else{
        //     TimeCycle.CheckClosingConditions();
        // }
    }
}