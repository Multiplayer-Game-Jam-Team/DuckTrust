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

    public void ShowPausePanel()
    {
        pausePanel.SetActive(true);
    }
    public void HidePausePanel()
    {
        pausePanel.SetActive(false);
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
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

}
