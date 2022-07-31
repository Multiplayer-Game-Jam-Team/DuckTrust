using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float speed = 10.0f;

    private Rigidbody _rb;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        Move();
    }

    private void Move() {
        if (InputManager.Instance.IsMoving) {
            Vector2 readDir = InputManager.Instance.MoveDirection;
            Vector3 direction = new Vector3(readDir.x,0,readDir.y);

            _rb.velocity = direction * speed;
        }
    }
}
