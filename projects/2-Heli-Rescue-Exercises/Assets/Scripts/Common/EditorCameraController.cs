using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;
using UnityEngine.SpatialTracking;

public class EditorCameraController : MonoBehaviour
{
    private float _yaw;
    private float _pitch;

    private void Start()
    {
        if (!Application.isEditor)
        {
            return;
        }

        var driver = FindObjectOfType<TrackedPoseDriver>();
        if (driver != null)
        {
            var provider = driver.gameObject.AddComponent<EditorPoseProvider>();
            provider.ControllerTransform = transform;
            driver.poseProviderComponent = provider;
        }
    }

    private void Update()
    {
        if (!Application.isEditor)
        {
            return;
        }

        var speed = 0.3f;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= 5;
        }

        var delta = Time.deltaTime * speed;
        var x = Input.GetAxis("Horizontal") * delta;
        var z = Input.GetAxis("Vertical") * delta;
        var y = Input.GetKey(KeyCode.Q) ? -delta : Input.GetKey(KeyCode.E) ? delta : 0.0f;

        transform.Translate(x, y, z);

        if (Input.GetMouseButton(1))
        {
            var lookDelta = Time.deltaTime * 100.0f;
            _yaw += Input.GetAxis("Mouse X") * lookDelta;
            _pitch = Mathf.Clamp(_pitch - Input.GetAxis("Mouse Y") * lookDelta, -90f, 90f);

            transform.eulerAngles = new Vector3(_pitch, _yaw, 0.0f);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    public class EditorPoseProvider : BasePoseProvider
    {
        public Transform ControllerTransform;

        public override bool TryGetPoseFromProvider(out Pose output)
        {
            if (ControllerTransform != null)
            {
                output.position = ControllerTransform.position;
                output.rotation = ControllerTransform.rotation;
            }
            else
            {
                output.position = Vector3.zero;
                output.rotation = Quaternion.identity;
            }

            return true;
        }
    }
}