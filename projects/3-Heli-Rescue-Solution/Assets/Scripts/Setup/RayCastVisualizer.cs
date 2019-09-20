using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class RayCastVisualizer : MonoBehaviour
{
    public GameObject CursorPrefab;
    public Pose? Pose => _pose;

    private GameObject _cursor;
    private LayerMask _mask;
    private Pose? _pose;
    private ARRaycastManager _raycastManager;
    private ARPlaneManager _planeManager;

    void Start()
    {
        _mask = LayerMask.GetMask("DebugPlane");
        _raycastManager = FindObjectOfType<ARRaycastManager>();
        _planeManager = FindObjectOfType<ARPlaneManager>();
        _cursor = Instantiate(CursorPrefab);
        HideCursor();
    }

    void Update()
    {
        var ray = new Ray(CameraCache.Transform.position, CameraCache.Transform.forward);

        if (Application.isEditor)
        {
            if (Physics.Raycast(ray, out var hitInfo, 100, _mask))
            {
                ShowCursorAt(hitInfo.point);
                return;
            }
        }
        else
        {
            // Exercise:
            var hits = new List<ARRaycastHit>();
            if (_raycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
            {
                var closestHit = hits
                    .Where(hit => _planeManager.GetPlane(hit.trackableId).IsValid())
                    .OrderBy(hit => hit.distance)
                    .FirstOrNull();
                
                if (closestHit.HasValue)
                {
                    ShowCursorAt(closestHit.Value.pose.position);
                    return;
                }
                
//                for (var i = 0; i < hits.Count; i++)
//                {
//                    var hit = hits[i];
//                    var plane = _planeManager.GetPlane(hit.trackableId);
//                    if (plane.IsValid())
//                    {
//                        ShowCursorAt(hit.pose.position);
//                        return;
//                    }
//                }
            }
        }

        HideCursor();
    }

    private void ShowCursorAt(Vector3 cursorPos)
    {
        if (!_cursor.activeSelf)
        {
            _cursor.SetActive(true);
        }

        var cameraPos = CameraCache.Transform.position;
        _cursor.transform.position = cursorPos;
        _cursor.transform.LookAt(new Vector3(cameraPos.x, cursorPos.y, cameraPos.z));
        _pose = _cursor.transform.ToPose();
    }

    private void HideCursor()
    {
        _cursor.SetActive(false);
        _pose = null;
    }
}