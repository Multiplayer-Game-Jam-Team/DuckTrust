using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ControlsPanel : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField]
    private float timeToDisappear = 30.0f;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(StartDisappearCountdown(timeToDisappear));
    }

    private IEnumerator StartDisappearCountdown(float time)
    {
        yield return new WaitForSeconds(time);
        _animator.SetTrigger("Close");
    }
}
