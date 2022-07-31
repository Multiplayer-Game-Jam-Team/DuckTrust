using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerNumber
    {
        Player1,
        Player2
    }

    [Header("Player Number")]
    [SerializeField]
    private PlayerNumber playerNumber;

    [Header("Movement Settings")]
    [SerializeField]
    private float moveSpeed = 10.0f;
    [SerializeField]
    private float rotationSpeed = 1.0f;
    
    [Header("References")]
    [SerializeField]
    private GameObject platform;
    [SerializeField]
    private GameObject duckModel;

    private Rigidbody _rb;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //duckModel.transform.right = Vector3.RotateTowards(duckModel.transform.right, _rb.velocity.normalized, rotationSpeed, 0.0f);
    }

    private void FixedUpdate() {
        if(playerNumber == PlayerNumber.Player1)
        {
            Move(InputManager.Instance.IsMovingPlayer1, InputManager.Instance.MoveDirectionPlayer1);
        }
        else if (playerNumber == PlayerNumber.Player2)
        {
            Move(InputManager.Instance.IsMovingPlayer2, InputManager.Instance.MoveDirectionPlayer2);
        }
    }

    private void Move(bool isMoving, Vector2 moveDirection) {
        if (isMoving) {
            Vector2 readDir = moveDirection;
            Vector3 direction = new Vector3(readDir.x,0,readDir.y);

            _rb.MovePosition(_rb.position + direction.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionStay(Collision other) {
        if (other.gameObject.tag == "Platform") {
            transform.up = other.transform.up;
        }
    }
}
