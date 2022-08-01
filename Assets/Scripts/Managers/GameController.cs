using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{

    public float  YToDestroy { get => ObjectsOutDetector.transform.position.y; }

    [Header("References")]
    [SerializeField]
    private GameObject ObjectsOutDetector;
    [SerializeField]
    private GameObject PlayerPrefab;

    private int _playersAlive = 2;
    private Vector3?[] _playersPositions;
    private Player[] _players; 

    protected override void Awake()
    {
        base.Awake();
        _playersPositions = new Vector3?[2] {null,null};
        _players = new Player[2];
    }

    public void RegistrerPlayerPos(Vector3 pos)
    {
        if (_playersPositions[0] == null)
        {
            _playersPositions[0] = pos;
        }
        else if (_playersPositions[1] == null)
        {
            _playersPositions[1] = pos;
        }
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

    public void RegisterPlayer()
    {

    }

    private void GameOver()
    {
        for(int i = 0; i < _players.Length; i++)
        {
            Destroy(_players[i].gameObject);
            _players[i] = Instantiate(PlayerPrefab, (Vector3)_playersPositions[0], Quaternion.identity).GetComponent<Player>(); ;
        }
    }

}
