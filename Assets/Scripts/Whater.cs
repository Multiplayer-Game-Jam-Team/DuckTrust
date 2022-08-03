using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whater : MonoBehaviour
{
    [SerializeField]
    private float sinkSpeed;

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(rb.velocity.x, -sinkSpeed, rb.velocity.z);
    }
}
