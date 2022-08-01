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
    private GameObject platform;
    [SerializeField]
    private GameObject duckModel;

    [Header("Debug")]
    [SerializeField]
    private bool debug;

    private Rigidbody _rb;
    private Vector3 _debugRepulsiveForce;
    private bool _isTouchingPlatform;
    private bool _isAlive = true;

    private bool _canMove = true;
    private bool _canPressStone = true;
    private float _stoneTimer = 0.0f;
    private float _previousDuckMass = 0.0f;
    private Material _previousMaterial;
    private PhysicMaterial _previousPhysicMaterial;

    private void Awake() 
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _previousDuckMass = _rb.mass;
        _previousMaterial = duckModel.GetComponent<MeshRenderer>().material;
        _previousPhysicMaterial = gameObject.GetComponent<BoxCollider>().material;
    }

    private void Update()
    {
        if(transform.position.y < GameController.Instance.YToDestroy && _isAlive)
        {
            _isAlive = false;
            GameController.Instance.PlayerOut();
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
                    _stoneTimer = 0;
                    _canPressStone = false;
                    _canMove = true;
                    _rb.mass = _previousDuckMass;
                    duckModel.GetComponent<MeshRenderer>().material = _previousMaterial;
                    gameObject.GetComponent<BoxCollider>().material = _previousPhysicMaterial;
                  
                    StartCoroutine(COCooldownStone(stoneCooldown));
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
                    _stoneTimer = 0;
                    _canPressStone = false;
                    _canMove = true;
                    _rb.mass = _previousDuckMass;
                    duckModel.GetComponent<MeshRenderer>().material = _previousMaterial;
                    gameObject.GetComponent<BoxCollider>().material = _previousPhysicMaterial;
              
                    StartCoroutine(COCooldownStone(stoneCooldown));
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

    private void Stone(bool isPressingStone)
    {
        if (!isPressingStone || !_canPressStone)
            return;

        Debug.Log("Pressing Stone");

        _canMove = false;
        _rb.mass = stoneMass;
        duckModel.GetComponent<MeshRenderer>().material = stoneMaterial;
        gameObject.GetComponent<BoxCollider>().material = stonePhysicMaterial;
       
        transform.up = platform.transform.up;
        transform.rotation = Quaternion.LookRotation(transform.forward, transform.up);

        _stoneTimer += Time.deltaTime;
        if (_stoneTimer >= stoneTime)
        {
            Debug.Log("Pressing Overtime");
            _stoneTimer = 0;
            _canPressStone = false;
            _canMove = true;
            duckModel.GetComponent<MeshRenderer>().material = _previousMaterial;
            gameObject.GetComponent<BoxCollider>().material = _previousPhysicMaterial;
            _rb.mass = _previousDuckMass;
            StartCoroutine(COCooldownStone(stoneCooldown));
            
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
            _isTouchingPlatform = true;
        
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
