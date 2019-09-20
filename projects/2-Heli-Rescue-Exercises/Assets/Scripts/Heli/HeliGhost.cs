using UnityEngine;

public class HeliGhost : MonoBehaviour
{
    private Heli _heli;
    private Material _material;

    void Start()
    {
        _heli = FindObjectOfType<Heli>();
        _material = GetComponentInChildren<MeshRenderer>().sharedMaterial;
    }

    private void Update()
    {
        transform.position = CameraCache.Transform.TransformPoint(_heli.CameraOffset);

        var cameraForward = CameraCache.Transform.forward.ProjectXZ();
        var cameraForwardAngle = Vector3.SignedAngle(Vector3.forward, cameraForward, Vector3.up);
        transform.rotation = Quaternion.Euler(0, cameraForwardAngle, 0);

        var alpha = Mathf.Lerp(0.03f, 0.6f, Vector3.Distance(transform.position, _heli.transform.position));
        _material.color = new Color(_material.color.r, _material.color.g, _material.color.b, alpha);
    }
}