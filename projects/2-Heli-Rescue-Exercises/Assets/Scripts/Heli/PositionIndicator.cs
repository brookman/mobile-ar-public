using UnityEngine;

public class PositionIndicator : MonoBehaviour
{
    public OutlineRaycaster OutlineRaycaster;

    public GameObject Indicator;

    private void Update()
    {
        var obstacle = OutlineRaycaster.FindTopmostObstacle();
        if (obstacle.HasValue)
        {
            Indicator.transform.rotation = Quaternion.identity;
            Indicator.transform.position = obstacle.Value;
            Indicator.SetActive(true);
        }
        else
        {
            Indicator.SetActive(false);
        }
    }
}