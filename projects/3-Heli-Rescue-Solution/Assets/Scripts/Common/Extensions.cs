using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public static class Extensions
{
    public static Vector3 ProjectXZ(this Vector3 v)
    {
        return Vector3.ProjectOnPlane(v, Vector3.up);
    }

    public static Pose ToPose(this Transform transform)
    {
        return new Pose(transform.position, transform.rotation);
    }

    public static Pose ToLocalPose(this Transform transform)
    {
        return new Pose(transform.localPosition, transform.localRotation);
    }

    public static void ApplyPose(this Transform transform, Pose pose)
    {
        transform.position = pose.position;
        transform.rotation = pose.rotation;
    }

    public static float Area(this ARPlane arPlane)
    {
        return arPlane.size.x * arPlane.size.y;
    }

    public static bool IsValid(this ARPlane arPlane)
    {
        return arPlane.alignment == PlaneAlignment.HorizontalUp && arPlane.Area() > 0.75f;
    }
    
    public static T? FirstOrNull<T>(this IEnumerable<T> sequence) where T : struct
    {
        foreach (var item in sequence)
        {
            return item;
        }
        return null;
    }
}