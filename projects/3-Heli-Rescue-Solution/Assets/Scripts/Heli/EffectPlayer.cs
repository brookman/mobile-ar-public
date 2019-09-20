using UnityEngine;

public class EffectPlayer : MonoBehaviour
{
    public GameObject Pickup;
    public GameObject Dropoff;

    private static EffectPlayer _instance;
    public static EffectPlayer Instance => _instance;

    private void Awake()
    {
        _instance = this;
    }

    public void PlayPickup(Vector3 position)
    {
        PlayAnimation(Pickup, position);
    }

    public void PlayDropoff(Vector3 position)
    {
        PlayAnimation(Dropoff, position);
    }

    public void PlayAnimation(GameObject AnimationRoot, Vector3 position)
    {
        var instance = Instantiate(AnimationRoot);
        instance.transform.position = position;

        var callbackHandler = instance.GetComponentInChildren<AlertObservers>();
        callbackHandler.Subscribe((v) =>
        {
            if (v == "finished")
            {
                Destroy(instance.gameObject);
            }
        });

        var animation = instance.GetComponentInChildren<Animation>();
        animation.Stop();
        animation.Rewind();
        animation.Play();
    }
}