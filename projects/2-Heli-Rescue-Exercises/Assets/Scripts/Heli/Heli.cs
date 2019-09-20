using UnityEngine;

public class Heli : MonoBehaviour
{
    public OutlineRaycaster OutlineRaycaster;
    public readonly Vector3 CameraOffset = new Vector3(0, 0.05f, 0.5f);
    private const float Speed = 0.8f; // smaller is faster
    private const float LeanFactor = 150f;
    private const float LeanMax = 45f;
    private const float TurnFactor = 1f;

    private bool hasPersonLoaded = false;

    public GameObject PersonPrefab;
    public GameObject DropoffArea;

    private Vector3 _velocity;
    private float _rotationY = 0;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        var heliTrans = transform;
        var target = CameraCache.Transform.TransformPoint(CameraOffset);
        var obstacle = OutlineRaycaster.FindTopmostObstacle();
        if (obstacle.HasValue)
        {
            target = new Vector3(target.x, Mathf.Max(target.y, obstacle.Value.y + 0.035f), target.z);
        }

        var pos = Vector3.SmoothDamp(heliTrans.position, target, ref _velocity, Speed);

        transform.position = pos;

        var rPlane = heliTrans.right.ProjectXZ();
        var fPlane = heliTrans.forward.ProjectXZ();

        var projectedRight = Vector3.Project(_velocity, rPlane);
        var projectedForward = Vector3.Project(_velocity, fPlane);

        var rMag = projectedRight.magnitude * Mathf.Sign(Vector3.Dot(rPlane, projectedRight)) * -1f;
        var fMag = projectedForward.magnitude * Mathf.Sign(Vector3.Dot(fPlane, projectedForward));

        var cameraForward = CameraCache.Transform.forward.ProjectXZ();
        var cameraForwardAngle = Vector3.SignedAngle(Vector3.forward, cameraForward, Vector3.up);
        _rotationY = Mathf.LerpAngle(_rotationY, cameraForwardAngle, Time.deltaTime * TurnFactor);

        var rot = Quaternion.AngleAxis(Clamp(rMag * LeanFactor), Vector3.forward) * Quaternion.identity;
        rot = Quaternion.AngleAxis(Clamp(fMag * LeanFactor), Vector3.right) * rot;
        rot = Quaternion.AngleAxis(_rotationY, Vector3.up) * rot;
        heliTrans.rotation = rot;

        _audioSource.pitch = 1 + Mathf.Clamp(_velocity.magnitude * 1.5f, 0, 2);
    }

    public void DropoffPerson()
    {
        if (!hasPersonLoaded) return;
        hasPersonLoaded = false;

        PlacePerson(Instantiate(PersonPrefab));
    }

    public void PickupPerson(GameObject person)
    {
        if (hasPersonLoaded) return;
        hasPersonLoaded = true;

        EffectPlayer.Instance.PlayPickup(person.transform.position);
        
        person.gameObject.SetActive(false);
        person.transform.SetParent(null);
        
        Destroy(person);
    }

    public void PlacePerson(GameObject gameObject, float spacing = 0.02f, int gridWidth = 3)
    {
        GridPlacer.Place(gameObject, DropoffArea.transform, spacing, gridWidth);
        gameObject.transform.localScale = Vector3.one;

        GameController.Instance.PersonSaved();
    }


    private static float Clamp(float value)
    {
        return Mathf.Clamp(value, -LeanMax, LeanMax);
    }
}