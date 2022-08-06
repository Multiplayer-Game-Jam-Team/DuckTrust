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
    [SerializeField]
    private float rainbowSpeed = 2f;
    [SerializeField]
    private float rainbowAlpha = 1f;

    private bool _newBest = false;

    private void Update()
    {
        if (_newBest)
        {
            timeText.color = HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.time * rainbowSpeed, 1), 1, 1, rainbowAlpha));
            bestTimeText.color = HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.time * rainbowSpeed, 1), 1, 1, rainbowAlpha));
        }   
    }

    public void SetTimeAndBestTime(string time, string bestTime)
    {
        if(time.Equals(bestTime)){
            timeText.text = "New best time !";
            bestTimeText.text = bestTime;
            _newBest = true;
        }
        else {
            timeText.text = "Time: " + time;
            bestTimeText.text = "Best Time: " + bestTime;
        }
        
    }
}
