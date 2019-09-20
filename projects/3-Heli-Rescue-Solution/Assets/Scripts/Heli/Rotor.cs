using UnityEngine;

public class Rotor : MonoBehaviour
{
    public Vector3 RotationAxis;
    public float RotationsPerSecond;

    void Update()
    {
        transform.Rotate(RotationAxis, 360f * RotationsPerSecond * Time.deltaTime);
    }
}