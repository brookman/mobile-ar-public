using UnityEngine;

public class HeliPickupHandler : MonoBehaviour
{
    public Heli Heli;

    public void OnTriggerEnter(Collider collider)
    {
        var colliderGameObject = collider.gameObject;
        var isPickupArea = colliderGameObject.layer == LayerMask.NameToLayer("Pickup");
        var isDropoffArea = colliderGameObject.layer == LayerMask.NameToLayer("Dropoff");

        if (isPickupArea)
        {
            var pickup = colliderGameObject.transform.parent.GetComponentInChildren<Spawner>().gameObject;
            if (pickup == null)
            {
                Debug.LogError("Can not find spawner on colliding gameobject");
            }

            if (pickup.transform.childCount == 0)
            {
                Debug.Log("Cant find people to pick up");
            }

            Heli.PickupPerson(pickup.transform.GetChild(0).gameObject);
        }
        else if (isDropoffArea)
        {
            Heli.DropoffPerson();
        }
    }
}