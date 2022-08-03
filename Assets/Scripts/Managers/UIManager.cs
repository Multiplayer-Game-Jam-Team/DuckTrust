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

}
