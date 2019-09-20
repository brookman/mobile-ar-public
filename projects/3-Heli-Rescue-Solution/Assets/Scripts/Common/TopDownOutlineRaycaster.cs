using UnityEngine;

public class TopDownOutlineRaycaster
{
    private static LayerMask? _mask;

    private static LayerMask Mask
    {
        get
        {
            if (!_mask.HasValue)
            {
                _mask = LayerMask.GetMask("Obstacle");
            }

            return _mask.Value;
        }
    }

    public struct TopmostHit
    {
        public Vector3? Hit;

        public void UpdateWith(Vector3? newHit)
        {
            if (!Hit.HasValue || (newHit.HasValue && newHit.Value.y > Hit.Value.y))
            {
                Hit = newHit;
            }
        }
    }

    public static Vector3? FindTopmostObstacle(Transform center, params Transform[] outline)
    {
        var outlinePositions = new Vector3[outline.Length];
        for (var i = 0; i < outline.Length; i++)
        {
            outlinePositions[i] = outline[i].position;
        }

        return FindTopmostObstacle(center.position, outlinePositions);
    }

    public static Vector3? FindTopmostObstacle(Vector3 center, params Vector3[] outline)
    {
        Debug.DrawLine(center, center + Vector3.down, Color.green);
        for (var i = 0; i < outline.Length; i++)
        {
            Debug.DrawLine(outline[i], outline[i] + Vector3.down, Color.green);
        }

        var topmostHit = new TopmostHit();
        topmostHit.UpdateWith(GetHit(center));
        for (var i = 0; i < outline.Length; i++)
        {
            topmostHit.UpdateWith(GetHit(outline[i]));
        }

        if (topmostHit.Hit.HasValue)
        {
            return new Vector3(center.x, topmostHit.Hit.Value.y, center.z);
        }

        return null;
    }

    private static Vector3? GetHit(Vector3 v)
    {
        if (Physics.Raycast(ToRay(v), out var hitInfo, 100, Mask))
        {
            return hitInfo.point;
        }

        return null;
    }

    private static Ray ToRay(Vector3 v)
    {
        return ToRay(v, Vector3.down);
    }

    private static Ray ToRay(Vector3 v, Vector3 dir)
    {
        var dirNormalized = dir.normalized;
        return new Ray(v - dirNormalized * 10, dirNormalized);
    }
}