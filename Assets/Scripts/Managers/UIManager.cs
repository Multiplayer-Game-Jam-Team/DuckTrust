using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("References")]
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private GameObject gamePanel;
    [SerializeField]
    private StoneCooldownIcon stoneCooldownIconPlayer1;
    [SerializeField]
    private StoneCooldownIcon stoneCooldownIconPlayer2;

    public void ShowPausePanel()
    {
        pausePanel.SetActive(true);
    }
    public void HidePausePanel()
    {
        pausePanel.SetActive(false);
    }

    public void ShowGameOverPanel(string time, string bestTime)
    {
        gameOverPanel.SetActive(true);
        gameOverPanel.GetComponent<GameOverPanel>().SetTimeAndBestTime(time, bestTime);
    }
    public void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }
    public void ShowGamePanel()
    {
        gamePanel.SetActive(true);
    }
    public void HideGamePanel()
    {
        gamePanel.SetActive(false);
    }

    public void StartCooldownForPlayer(Player.PlayerNumber number)
    {
        if(number == Player.PlayerNumber.Player1)
            stoneCooldownIconPlayer1.StartWaiting();
        else
            stoneCooldownIconPlayer2.StartWaiting();
    }
}
