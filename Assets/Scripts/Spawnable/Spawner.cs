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
    private float minSpawnTime = 2.0f;
    [SerializeField]
    private float maxSpawnTime = 5.0f;
    [SerializeField]
    private float maxLimit = 1.0f;
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
        _stop = !active;
        if (_stop)
            StopAllCoroutines();
    }

    protected override void Awake()
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
            float spawnRate = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnRate);
            Spawn();

            if ((maxSpawnTime - decrementStep) > maxLimit)
            {
                maxSpawnTime -= decrementStep;
            }
            else maxSpawnTime = maxLimit;
        }
    }


}
