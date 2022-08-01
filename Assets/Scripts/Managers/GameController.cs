using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{

    public float  YToDestroy { get => ObjectsOutDetector.transform.position.y; }

    [Header("Properties")]
    [SerializeField]
    private GameObject ObjectsOutDetector;

    private int _playersAlive = 2;

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

    }

}
