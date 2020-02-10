using UnityEngine;

public class HeliCollider : MonoBehaviour
{
    public GameObject CrashEffectPrefab;

    private LayerMask _layer;

    private void Start()
    {
        _layer = LayerMask.NameToLayer("Obstacle");
    }

    public void OnTriggerEnter(Collider collider)
    {
        // Exercise: Check for collision and crash helicopter
        // Hints:
        // - Check if the collider object is on the correct layer called "Obstacle"
        if (collider.gameObject.layer != _layer) return;
        // - Instantiate CrashEffectPrefab
        Instantiate(CrashEffectPrefab);
        // - Call GameController.Instance.Crash()
        GameController.Instance.Crash();
    }
}