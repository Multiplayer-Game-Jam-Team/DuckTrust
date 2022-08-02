using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("References")]
    [SerializeField]
    private GameObject pausePanel;
    
    public void ShowPausePanel()
    {
        pausePanel.SetActive(true);
    }
    public void HidePausePanel()
    {
        pausePanel.SetActive(false);
    }
}
