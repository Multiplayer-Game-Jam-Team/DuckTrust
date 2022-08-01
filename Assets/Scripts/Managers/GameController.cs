using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{

    public float  YToDestroy { get => ObjectsOutDetector.transform.position.y; }
    public GameObject Lilly { get => lilly; }

    [Header("References")]
    [SerializeField]
    private GameObject ObjectsOutDetector;
    [SerializeField]
    private GameObject PlayerPrefab;
    [SerializeField]
    private GameObject lilly;

    private int _playersAlive = 2;
    private Vector3?[] _playersStartingPositions;
    private Player[] _players; 

    protected override void Awake()
    {
        base.Awake();
        _playersStartingPositions = new Vector3?[2] {null,null};
        _players = new Player[2];
    }

    public void RegisterPlayer(Player player)
    {

        if(player.PlayerType == Player.PlayerNumber.Player1)
        {
            _players[0] = player;
            if (_playersStartingPositions[0] == null)
                _playersStartingPositions[0] = player.transform.position;
        }
        else if(player.PlayerType == Player.PlayerNumber.Player2)
        {
            _players[1] = player;
            if (_playersStartingPositions[1] == null)
                _playersStartingPositions[1] = player.transform.position;
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

    private void GameOver()
    {
        for(int i = 0; i < _players.Length; i++)
        {
            Destroy(_players[i].gameObject);
            _players[i] = Instantiate(PlayerPrefab, (Vector3)_playersStartingPositions[i], Quaternion.identity).GetComponent<Player>();
            if(i == 0)
                _players[i].PlayerType = Player.PlayerNumber.Player1;
            else if (i==1)
                _players[i].PlayerType = Player.PlayerNumber.Player2;
            Lilly.transform.rotation = Quaternion.identity;
        }
        _playersAlive = 2;
    }

}
