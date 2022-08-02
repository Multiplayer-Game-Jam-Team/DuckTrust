using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{

    public float  YToDestroy { get => objectsOutDetector.transform.position.y; }
    public GameObject Lily { get => lily; }

    [Header("References")]
    [SerializeField]
    private GameObject objectsOutDetector;
    [SerializeField]
    private GameObject lily;

    private int _playersAlive = 2;
    private Player[] _players; 
    private bool _isGamePaused = false;

    protected override void Awake()
    {
        base.Awake();
        _players = new Player[2];
    }

    public void PauseButtonPressed()
    {
        Debug.Log("pause pressed");
        if (_isGamePaused) UnpauseGame();
        else PauseGame();
    }
    private void PauseGame()
    {
        if (!_isGamePaused)
        {
            Debug.Log("pause game called");
            _isGamePaused = true;
            Time.timeScale = 0;
            UIManager.Instance.ShowPausePanel();
        }
    }
    private void UnpauseGame()
    {
        if (_isGamePaused)
        {
            Debug.Log("unpause game called");
            UIManager.Instance.HidePausePanel();
            Time.timeScale = 1;
            _isGamePaused = false;
        }
    }
    public void QuitApplication()
    {
        Application.Quit();
    }

    public void RegisterPlayer(Player player)
    {

        if(player.PlayerType == Player.PlayerNumber.Player1)
            _players[0] = player;
        else if(player.PlayerType == Player.PlayerNumber.Player2)
            _players[1] = player;
    }

    public void PlayerOut()
    {
        Debug.Log("player out");
        _playersAlive--;
        if (_playersAlive <= 0)
        {
            Debug.Log("Gameover");
            GameOver();
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene(0);
    }
}
