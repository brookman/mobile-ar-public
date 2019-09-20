using UnityEngine;

public static class GridPlacer
{
    public static void Place(GameObject gameObject, Transform parentTransform, float spacing = 0.02f, int gridWidth = 3)
    {
        var cargoTransform = gameObject.transform;
        var placingIndex = parentTransform.childCount;
        cargoTransform.SetParent(parentTransform);
        cargoTransform.position = parentTransform.position + new Vector3(
                                      spacing * Mathf.Floor((float) placingIndex / gridWidth),
                                      0,
                                      spacing * (placingIndex % gridWidth));

        EffectPlayer.Instance?.PlayDropoff(cargoTransform.position);
        gameObject.SetActive(true);
    }
}