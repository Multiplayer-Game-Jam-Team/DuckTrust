using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectsOutDetectorCustomEditor : MonoBehaviour
{
    [CustomEditor(typeof(ObjectsOutDetector), true)]
    public class SpawnerCustomEditor : Editor
    {
        private void OnSceneGUI()
        {
            ObjectsOutDetector objectsOutDetector = (ObjectsOutDetector)target;
            Handles.color = Color.red;

            Vector3 position = new Vector3(objectsOutDetector.transform.position.x, objectsOutDetector.transform.position.y, objectsOutDetector.transform.position.z);
            Handles.DrawWireArc(position, Vector3.up, Vector3.forward, 360, objectsOutDetector.GizmosRadius);
        }

    }
}
