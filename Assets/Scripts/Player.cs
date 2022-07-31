using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public enum PlayerNumber {
        Player1,
        Player2
    }

    //------------------------------
    [Header("Player Number")]
    [SerializeField]
    private PlayerNumber playerNumber;
    //------------------------------
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

    Vector3 debugVector;
    private void Move(bool isMoving, Vector2 moveDirection) {
        if (isMoving) {
            Vector2 inputDirection = moveDirection;
            Vector3 WorldDirection = new Vector3(inputDirection.x,0, inputDirection.y);
            Vector3 DuckPlaneDirection = Vector3.ProjectOnPlane(WorldDirection, transform.up);
            DuckPlaneDirection += transform.position;
            debugVector = DuckPlaneDirection;

            _rb.MovePosition(_rb.position + WorldDirection.normalized * moveSpeed * Time.fixedDeltaTime);
            //_rb.velocity = DuckPlaneDirection.normalized * moveSpeed;

            //transform.right = Vector3.RotateTowards(transform.right, DuckPlaneDirection, rotationSpeed, 0.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Vector3 direction = (transform.position - collision.gameObject.transform.position).normalized;

            if (debug)
                Debug.Log("Repulsive Force direction: " + direction+" for "+this.gameObject.name);

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
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, debugVector);
        }
    }
}
