using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public enum PlayerNumber {
        Player1,
        Player2
    }

    public PlayerNumber PlayerType { get => playerNumber; }
    public bool IsStone { get; private set; }
    public bool CanPressStone { get => _canPressStone; }
    public bool IsAlive { get; set; } = true;
    public float StoneCooldown { get => stoneCooldown; }

    public bool IsTouchingPlatform { get => _isTouchingPlatform; }

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

    [Header("Stone Settings")]
    [SerializeField]
    private float stoneTime = 5.0f;
    [SerializeField]
    private float stoneCooldown = 20.0f;
    [SerializeField]
    private float stoneMass = 1.5f;
    [SerializeField]
    private Material stoneMaterial;
    [SerializeField]
    private PhysicMaterial stonePhysicMaterial;

    [Header("References")]
    [SerializeField]
    private GameObject duckModel;
    [SerializeField]
    private ParticleSystem particles;

    [Header("Debug")]
    [SerializeField]
    private bool debug;

    private GameObject _platform;
    private Rigidbody _rb;
    private Vector3 _debugRepulsiveForce;
    private bool _isTouchingPlatform;

    private bool _canMove = true;
    private bool _canPressStone = true;
    private float _stoneTimer = 0.0f;
    private float _previousDuckMass = 0.0f;
    private Material _yellowMaterial;
    private PhysicMaterial _yellowPhysicsMaterial;

    private MeshRenderer _meshRenderer;
    private BoxCollider _boxCollider;

    public void StopMove()
    {
        _canMove = false;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _meshRenderer = duckModel.GetComponent<MeshRenderer>();
        _boxCollider = gameObject.GetComponent<BoxCollider>();
    }

    private void Start()
    {
        _platform = GameController.Instance.Lily;
        _previousDuckMass = _rb.mass;
        _yellowMaterial = _meshRenderer.material;
        _yellowPhysicsMaterial =_boxCollider.material;
        
        Debug.Log("my number is: " + playerNumber.ToString());

        GameController.Instance.RegisterPlayer(this);
    }

    //private bool 
    private void Update()
    {
        if (!IsAlive)
        {
            _canMove = false;
            _canPressStone = false;
        }
        if (playerNumber == PlayerNumber.Player1)
        {
            if (InputManager.Instance.IsStonePlayer1)
            {
                Stone(true);
            }
            else
            {
                if (_canPressStone && _stoneTimer > 0.0f)
                {
                    Debug.Log("STONE IS NO LONGER PRESSED");
                    UIManager.Instance.StartCooldownForPlayer(playerNumber);

                    if (IsStone)
                    {
                        _stoneTimer = 0;
                        _canPressStone = false;
                        _canMove = true;
                        _rb.mass = _previousDuckMass;
                        _meshRenderer.material = _yellowMaterial;
                        _boxCollider.material = _yellowPhysicsMaterial;
                        IsStone = false;
                        StartCoroutine(COCooldownStone(stoneCooldown));
                    }
                }
            }
        }
        else if (playerNumber == PlayerNumber.Player2)
        {
            if (InputManager.Instance.IsStonePlayer2)
            {
                Stone(true);
            }
            else
            {
                if (_canPressStone && _stoneTimer > 0.0f)
                {
                    Debug.Log("STONE IS NO LONGER PRESSED");
                    UIManager.Instance.StartCooldownForPlayer(playerNumber);
                    if (IsStone)
                    {
                        _stoneTimer = 0;
                        _canPressStone = false;
                        _canMove = true;
                        _rb.mass = _previousDuckMass;
                        _meshRenderer.material = _yellowMaterial;
                        _boxCollider.material = _yellowPhysicsMaterial;
                        IsStone = false;
                        StartCoroutine(COCooldownStone(stoneCooldown));
                    }
                }
            }
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
        if (!isMoving || !_canMove)
            moveDirection = lastMoveDirection;

        lastMoveDirection = moveDirection;
        Vector2 inputDirection = moveDirection;
        Vector3 WorldDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
        Vector3 LillyPlaneDirection = Vector3.ProjectOnPlane(WorldDirection, _platform.transform.up);
        LillyPlaneDirection += transform.position;

        if (_canMove && isMoving)
            _rb.MovePosition(_rb.position + WorldDirection.normalized * moveSpeed * Time.fixedDeltaTime);

        Vector3 upToUse;
        if (_isTouchingPlatform) upToUse = _platform.transform.up;
        else upToUse = transform.up;

        Quaternion toRot = Quaternion.LookRotation(transform.position - LillyPlaneDirection,upToUse);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRot, rotationSpeed * Time.fixedDeltaTime);
    }

    Vector2 lastMoveDirection;
    private void Stone(bool isPressingStone)
    {
        if (!isPressingStone || !_canPressStone)
            return;

        Debug.Log("Pressing Stone");

        if (!IsStone)
        {
            IsStone = true;
            _canMove = false;
            _rb.mass = stoneMass;
            _meshRenderer.material = stoneMaterial;
            _boxCollider.material = stonePhysicMaterial;
        }
        
        Move(false, lastMoveDirection);
        _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
        UIManager.Instance.FillCooldownForPlayer(playerNumber);

        _stoneTimer += Time.deltaTime;

        if (_stoneTimer >= stoneTime)
        {
            Debug.Log("Pressing Overtime");
            UIManager.Instance.StartCooldownForPlayer(playerNumber);
            _stoneTimer = 0;
            _canPressStone = false;
            _canMove = true;
            _meshRenderer.material = _yellowMaterial;
            _boxCollider.material = _yellowPhysicsMaterial;
            _rb.mass = _previousDuckMass;
            StartCoroutine(COCooldownStone(stoneCooldown));
            _rb.freezeRotation = false;
            IsStone = false;
            return;
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

    private void OnCollisionStay(Collision collision) 
    {
        if (collision.gameObject.tag.Equals("Platform"))
        {
            _isTouchingPlatform = true;
        }
        else
        {
            _isTouchingPlatform = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (!debug)
            return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _debugRepulsiveForce);
    }
    private IEnumerator COCooldownStone(float cooldown)
    {
        Debug.Log("STONE ON CD");
        yield return new WaitForSeconds(cooldown);
        _canPressStone = true;
        Debug.Log("STONE CD FINISHED");
    }

}
