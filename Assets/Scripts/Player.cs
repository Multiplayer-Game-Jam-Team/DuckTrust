using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private float rotationSpeed = 1.0f;
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
        transform.up = platform.transform.up;
        //duckModel.transform.forward = Vector3.RotateTowards(duckModel.transform.forward, _rb.velocity.normalized, rotationSpeed, 0.0f);
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        if (InputManager.Instance.IsMoving) {
            Vector2 readDir = InputManager.Instance.MoveDirection;
            Vector3 direction = new Vector3(readDir.x,0,readDir.y);

            _rb.MovePosition(_rb.position + direction.normalized * speed * Time.fixedDeltaTime);
        }
    }
}
