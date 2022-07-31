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
            Vector2 readDir = InputManager.Instance.MoveDirection;
            Vector3 direction = new Vector3(readDir.x,0,readDir.y);

            myRigidbody.velocity = direction * speed;
            
        }
    }
}
