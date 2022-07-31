using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObject : MonoBehaviour
{
    public int ProbabilityWeight { get => probabilityWeight; }

    [Header("Settings")]
    [SerializeField]
    private int probabilityWeight;
    private Rigidbody _rb;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player") {
            Player otherPlayer = other.gameObject.GetComponent<Player>();
            Vector3 direction = (transform.position - otherPlayer.transform.position).normalized;
            Vector3 force = direction * otherPlayer.RepulsiveForce;
            _rb.AddForce(force, ForceMode.Impulse);
        }
    }

}
