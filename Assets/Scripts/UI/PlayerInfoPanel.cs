using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfoPanel : MonoBehaviour
{
    public bool IsActive { get; private set; }

    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI playerText;
    [SerializeField]
    private Image triangleIcon;

    [Header("Settings")]
    [SerializeField]
    private Color playerColor = Color.white;

    private Player.PlayerNumber _playerNumber;

    public void SetActivePlayerInfo(bool active)
    {
        IsActive = active;
        playerText.enabled = active;
        triangleIcon.enabled = active;
    }

    private void Start()
    {
        // Get player number
        _playerNumber = GetComponentInParent<Player>().PlayerType;

        // Set text
        if(_playerNumber == Player.PlayerNumber.Player1)
        {
            playerText.text = "P1";
        }
        else if(_playerNumber == Player.PlayerNumber.Player2)
        {
            playerText.text = "P2";
        }

        // Set color
        playerText.color = playerColor;
        triangleIcon.color = playerColor;

        SetActivePlayerInfo(true);
    }
}
