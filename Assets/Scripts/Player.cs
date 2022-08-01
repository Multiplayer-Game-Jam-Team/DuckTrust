using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public enum PlayerNumber {
        Player1,
        Player2
    }

    public PlayerNumber PlayerType { get => playerNumber; }

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

    [Header("Debug")]
    [SerializeField]
    private bool debug;

    private Rigidbody _rb;
    private Vector3 _debugRepulsiveForce;
    private bool _isTouchingPlatform;
    private bool _isAlive = true;

    private void Awake() 
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(transform.position.y < GameController.Instance.YToDestroy && _isAlive)
        {
            _isAlive = false;
            GameController.Instance.PlayerOut();
        }
    }

    private void FixedUpdate() 
    {
        if (playerNumber == PlayerNumber.Player1)
        {
            Move(InputManager.Instance.IsMovingPlayer1, InputManager.Instance.MoveDirectionPlayer1);
        }
        else if (playerNumber == PlayerNumber.Player2)
        {
            Move(InputManager.Instance.IsMovingPlayer2, InputManager.Instance.MoveDirectionPlayer2);
        }
    }

    private void Move(bool isMoving, Vector2 moveDirection) 
    {
        if (!isMoving)
            return;

        Vector2 inputDirection = moveDirection;
        Vector3 WorldDirection = new Vector3(inputDirection.x,0, inputDirection.y);
        Vector3 DuckPlaneDirection = Vector3.ProjectOnPlane(WorldDirection, transform.up);
        DuckPlaneDirection += transform.position;

        _rb.MovePosition(_rb.position + WorldDirection.normalized * moveSpeed * Time.fixedDeltaTime);

        Vector3 upToUse;
        if (_isTouchingPlatform) upToUse = platform.transform.up;
        else upToUse = transform.up;

        Quaternion toRot = Quaternion.LookRotation(transform.position - DuckPlaneDirection,upToUse);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRot, rotationSpeed * Time.fixedDeltaTime);
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

    private void OnCollisionStay(Collision collision) 
    {
        if (collision.gameObject.tag.Equals("Platform")) 
            _isTouchingPlatform = true;
        
    }

    private void OnDrawGizmos()
    {
        if (!debug)
            return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _debugRepulsiveForce);
    }
}
