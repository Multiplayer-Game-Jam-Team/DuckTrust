using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Spawner),true)]
public class SpawnerCustomEditor : Editor
{
    private void OnSceneGUI()
    {
        Spawner spawner = (Spawner)target;
        Handles.color = Color.red;

        Vector3 position = new Vector3(spawner.transform.position.x, spawner.SpawnHeight, spawner.transform.position.z);
        Handles.DrawWireArc(position, Vector3.up, Vector3.forward, 360, spawner.SpawnRadius);
    }

}
