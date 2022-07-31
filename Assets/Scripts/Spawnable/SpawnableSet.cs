using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Spawnable/Set", fileName = "SpawnableSet")]
public class SpawnableSet : ScriptableObject
{
    [SerializeField]
    private SpawnableObject[] spawnableObjects;

    public GameObject GetRandomObject()
    {
        // If no object, return blank
        if (spawnableObjects.Length == 0)
            return null;

        float total = 0.0f;
        for (int i = 0; i < spawnableObjects.Length; i++)
        {
            total += spawnableObjects[i].ProbabilityWeight;
        }

        float rand = Random.value;
        float prob = 0.0f;

        int count = spawnableObjects.Length - 1;
        for (int i = 0; i < count; i++)
        {
            prob += spawnableObjects[i].ProbabilityWeight / total;
            if (prob >= rand)
            {
                return spawnableObjects[i].gameObject;
            }
        }
        return spawnableObjects[count].gameObject;
    }
}
