using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI DayAndNightText;

    [Header("Limit Settings")]
    public bool hasLimit;
    public float timerLimit;

    float currentTime = 30;

    // Update is called once per frame
    void Update()
    {
        CountDown();
    }

    void CountDown()
    {
        currentTime -= Time.deltaTime;

        if (hasLimit && ( currentTime <= timerLimit))
        {
            currentTime = timerLimit;
            SetTimerText();
            enabled = false;
        }
        SetTimerText();
    }

     // Ÿ�̸ӷ� ǥ��
    void SetTimerText()
    {
        timerText.text = currentTime.ToString("0");
    }
}
