using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody myRigidbody;

    private void Awake() {
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void Update() {
        Move();
    }

    private void Move() {
        if (InputManager.Instance.IsMoving) {
            Vector2 direction = InputManager.Instance.MoveDirection;
            myRigidbody.velocity = direction.normalized * speed;
        }
    }
}
