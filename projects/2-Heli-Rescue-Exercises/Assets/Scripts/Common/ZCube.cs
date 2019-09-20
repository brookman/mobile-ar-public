using UnityEngine;

public class ZCube : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.up,Time.deltaTime * 90);
    }
}