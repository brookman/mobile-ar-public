using UnityEngine;
using UnityEngine.XR.ARFoundation;

public static class CameraCache
{
    private static Camera _camera;
    private static bool _cameraInitialized;
    private static Transform _transform;
    private static bool _transformInitialized;

    public static Camera Camera
    {
        get
        {
            if (!_cameraInitialized || _camera == null)
            {
                _camera = Object.FindObjectOfType<ARCameraManager>().GetComponent<Camera>();
                _cameraInitialized = true;
            }

            return _camera;
        }
    }

    public static Transform Transform
    {
        get
        {
            if (!_transformInitialized || _transform == null)
            {
                _transform = Camera.transform;
                _transformInitialized = true;
            }

            return _transform;
        }
    }

    public static void Reset()
    {
        _cameraInitialized = false;
        _transformInitialized = false;
    }
}