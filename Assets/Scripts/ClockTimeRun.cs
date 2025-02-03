using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClockTimeRun : MonoBehaviour
{
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;

    public static double Minute {get; private set;}
    public static double Hour {get; private set;}

    private float minuteIrl = 1f; //2s irl = 1min in-game
    private float timer;

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
        timer -= Time.deltaTime;

        while(timer <= 0){
            Minute+= 0.333333;
            OnMinuteChanged?.Invoke();

            if(Minute >= 60){
                Hour+= 1;
                Minute = 0;
                OnHourChanged?.Invoke();
            }

            timer = minuteIrl;
        }
    }
}
