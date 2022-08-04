using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Singleton<Spawner>
{
    public bool IsStopped { get => _stop; }
    public float SpawnRadius { get => spawnRadius; }
    public float SpawnHeight { get => spawnHeight; }

    [Header("References")]
    [SerializeField]
    private SpawnableSet spawnableSet;

    [Header("Spawn Settings")]
    [SerializeField]
    private float minSpawnRate = 2.0f;
    [SerializeField]
    private float maxSpawnRate = 5.0f;
    [SerializeField]
    private float maxRange = 1.0f;
    [SerializeField]
    private float decrementStep = 0.1f;
    [SerializeField]
    private float spawnRadius = 7.5f;
    [SerializeField]
    private float spawnHeight = 10.0f;

    private bool _stop;

    public void Spawn()
    {
        GameObject objToSpawn = spawnableSet.GetRandomObject();

        Vector3 spawnPosition = Random.insideUnitCircle * spawnRadius;

        spawnPosition = new Vector3(spawnPosition.x, spawnHeight, spawnPosition.z);

        Instantiate(objToSpawn, spawnPosition, Quaternion.identity);
    }

    public void SetActiveSpawn(bool active)
    {
        StopAllCoroutines();
        _stop = !active;
    }

    protected  override void Awake()
    {
        base.Awake();
        _stop = false;
    }

    private void Start()
    {
        StartCoroutine(COSpawnLoop());
    }

    private IEnumerator COSpawnLoop()
    {
        while (!_stop)
        {
            float spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
            yield return new WaitForSeconds(spawnRate);
            Spawn();
           
            if((maxSpawnRate-decrementStep) - minSpawnRate > maxRange)
            {
                maxSpawnRate -= decrementStep;
            }
        }
    }


}
