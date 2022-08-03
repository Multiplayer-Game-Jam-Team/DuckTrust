using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private TextMeshProUGUI bestTimeText;

    public void SetTimeAndBestTime(string time, string bestTime)
    {
        timeText.text = "Time: " + time;
        bestTimeText.text = "Best Time: " + bestTime;
    }
}
