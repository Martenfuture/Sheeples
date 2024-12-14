using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float TimeValue;
    public bool TimerStarted;

    public static Timer instance = null;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (TimerStarted) DisplayTime();
    }

    void DisplayTime()
    {
        TimeValue += Time.deltaTime;

        float minutes = Mathf.FloorToInt(TimeValue / 60);
        float secounds = Mathf.FloorToInt(TimeValue % 60);

        gameObject.GetComponent<TextMeshProUGUI>().text = string.Format("{0:00}:{1:00}", minutes, secounds);
    }
}
