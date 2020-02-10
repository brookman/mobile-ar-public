﻿using System.Collections.Generic;
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
            // Exercise: Perform a Raycast and find the nearest, "valid" AR plane using the ARRaycastManager.
            // Hints:
            ARRaycastHit? closestHit = null;
            var list = new List<ARRaycastHit>();
            // - Use _raycastManager.Raycast with TrackableType PlaneWithinPolygon
            _raycastManager.Raycast(ray, list, TrackableType.PlaneWithinPolygon);
            foreach (var arRaycastHit in list)
            {
                if (arRaycastHit.hitType != TrackableType.PlaneWithinPolygon) continue;
                // - Use the ARPlaneManager to get the plane from the hit
                var plane = _planeManager.GetPlane(arRaycastHit.trackableId);
                // - ARPlane.IsValid() is an extension method to check the validity
                if (!plane.IsValid()) continue;

                if (closestHit.HasValue && closestHit.Value.distance < arRaycastHit.distance) continue;
                closestHit = arRaycastHit;
            }

            if (closestHit.HasValue)
            {
              ShowCursorAt(closestHit.Value.pose.position);
              return;
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