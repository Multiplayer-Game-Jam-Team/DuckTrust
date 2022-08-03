using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneCooldownIcon : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject filler;
    [SerializeField]
    private Player player;

    private float _totalSpace = 0;
    private float _speed = 1.0f;

    public void StartWaiting()
    {
        filler.transform.localPosition = new Vector3(filler.transform.localPosition.x, 150.0f, 0.0f);
        _totalSpace = 150.0f;
        _speed = 150.0f / player.StoneCooldown;
    }

    private void Update()
    {
        if (_totalSpace <= 0)
            return;
        _totalSpace -= Time.deltaTime * _speed;
        filler.transform.localPosition = new Vector3(filler.transform.localPosition.x, -(1.0f - _totalSpace), 0.0f);
    }

}
