using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DucksOutDetector : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player" && other.GetComponent<Player>()!=null)
        {
            GameController.Instance.PlayerOut(other.GetComponent<Player>());
        }
            
    }
}
