using UnityEngine;

public class OutlineRaycaster : MonoBehaviour
{
    public Transform CenterPoint;
    public Transform[] EdgePoints;

    public Vector3? FindTopmostObstacle()
    {
        return TopDownOutlineRaycaster.FindTopmostObstacle(CenterPoint, EdgePoints);
    }
}