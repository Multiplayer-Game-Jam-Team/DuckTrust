using UnityEngine;

public class RotateSun : MonoBehaviour
{
    public float rotationPwer;


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationPwer, 0f , 0f);
    }
}
