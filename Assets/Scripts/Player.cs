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
    [SerializeField]
    private float repulsiveForce = 5.0f;

    [Header("References")]
    [SerializeField]
    private GameObject platform;
    [SerializeField]
    private GameObject duckModel;

    [Header("Debug")]
    [SerializeField]
    private bool debug;

    private Rigidbody _rb;
    private Vector3 _debugRepulsiveForce;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Vector3 hitPoint = collision.contacts[0].point;
            Vector3 direction = (transform.position - hitPoint).normalized;

            if(debug)
                Debug.Log("Repulsive Force direction: " + direction+" for "+this.gameObject.name);

            direction = Vector3.ProjectOnPlane(direction, platform.transform.up).normalized;
            Vector3 force = direction * repulsiveForce;

            _debugRepulsiveForce = force;
            _rb.AddForce(force,ForceMode.Impulse);
        }
    }

    private void OnCollisionStay(Collision collision) {
        if (collision.gameObject.tag.Equals("Platform")) {
            transform.up = collision.transform.up;
        }
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, _debugRepulsiveForce);
        }
    }
}
