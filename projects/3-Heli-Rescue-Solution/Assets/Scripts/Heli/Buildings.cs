using UnityEngine;

public class Buildings : MonoBehaviour
{
    public static Transform AnchorTransform;

    void Start()
    {
        if (AnchorTransform != null)
        {
            transform.SetParent(AnchorTransform, false);
        }
        else
        {
            transform.position = new Vector3(0, -0.5f, 1);
        }
    }
}