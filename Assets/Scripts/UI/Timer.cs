using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float CurrentTimer { get => _timer; }

    [SerializeField]
    private TextMeshPro textMesh;

    private float _timer;

    public string GetCurrentFormattedTimer()
    {
        float minutes = Mathf.FloorToInt(_timer / 60);
        float seconds = Mathf.FloorToInt(_timer % 60);

        string currentTime = string.Format("{00:00}{1:00}", minutes, seconds);
        string timeFormatted = "";
        timeFormatted = timeFormatted + currentTime[0] + currentTime[1] + ":" + currentTime[2] + currentTime[3];

        return timeFormatted;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        UpdateTimerDisplay(_timer);
    }

    private void ResetTimer()
    {
        _timer = 0.0f;
    }

    private void UpdateTimerDisplay(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currentTime = string.Format("{00:00}{1:00}",minutes,seconds);
        string timeFormatted = "";
        timeFormatted = timeFormatted + currentTime[0] + currentTime[1]+":"+currentTime[2]+currentTime[3];

        // TODO: Update TextMesh Here 
        Debug.Log(timeFormatted);
    }
}
