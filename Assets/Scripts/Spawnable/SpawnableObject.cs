using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObject : MonoBehaviour
{
    public int ProbabilityWeight { get => probabilityWeight; }

    [Header("Settings")]
    [SerializeField]
    private int probabilityWeight;

}
