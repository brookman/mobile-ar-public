using UnityEngine;

public class HeliCollider : MonoBehaviour
{
    public GameObject CrashEffectPrefab;

    // Exercise:
    private LayerMask _layer;

    private void Start()
    {
        _layer = LayerMask.NameToLayer("Obstacle");
    }

    public void OnTriggerEnter(Collider collider)
    {
        var colliderGameObject = collider.gameObject;
        if (colliderGameObject.layer != _layer)
        {
            return;
        }

        Instantiate(CrashEffectPrefab);
        GameController.Instance.Crash();
    }
}