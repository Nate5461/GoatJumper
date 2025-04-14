using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StopWatch : MonoBehaviour
{

    public TextMeshPro timerText;  // Reference to the UI Text component
    private float elapsedTime;
    private bool isRunning;

     void Start()
    {
        ResetStopwatch();
        StartStopwatch();
    }

    void Update()
    {
        if (isRunning && Time.timeScale > 0f)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    public void StartStopwatch()
    {
        isRunning = true;
    }

    public void StopStopwatch()
    {
        isRunning = false;
    }

    public void ResetStopwatch()
    {
        elapsedTime = 0f;
        UpdateTimerDisplay();
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 100) % 100);

        if (timerText != null)
        {
            timerText.text = $"{minutes:00}, {seconds:00}, {milliseconds:00}";
        }
        else
        {
            Debug.LogWarning("TimerText is not assigned in the Inspector!");
        }
    }
}
