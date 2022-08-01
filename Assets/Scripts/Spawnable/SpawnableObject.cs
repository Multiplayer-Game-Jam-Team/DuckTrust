using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObject : MonoBehaviour
{
    //------------------------
    public int ProbabilityWeight { get => probabilityWeight; }
    //------------------------
    [Header("Settings")]
    [SerializeField]
    private int probabilityWeight;
    [SerializeField]
    private float repulsiveForce;
    //------------------------
    private Rigidbody _rb;

    private const float Y_TO_DESTROY = -10.0f;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (transform.position.y < GameController.Instance.YToDestroy)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player") {
            Vector3 direction = (transform.position - other.transform.position).normalized;
            Vector3 force = direction * repulsiveForce;
            _rb.AddForce(force, ForceMode.Impulse);
        }
    }

}
