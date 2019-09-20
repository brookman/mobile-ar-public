using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CustomARPlane : MonoBehaviour
{
    public static bool SetupInProgress;

    public Color ValidColor;
    public Color InvalidColor;
    public Material SetupMaterial;
    public Material GameMaterial;
    
    private bool _setupInProgress;

    private ARPlane _plane;
    private Renderer _renderer;
    private LineRenderer _lineRenderer;

    void Start()
    {
        _plane = GetComponent<ARPlane>();
        _renderer = GetComponent<Renderer>();
        _lineRenderer = GetComponent<LineRenderer>();
        UpdateRenderer();
    }

    void Update()
    {
        if (SetupInProgress != _setupInProgress)
        {
            _setupInProgress = SetupInProgress;
            UpdateRenderer();
        }

        if (_setupInProgress)
        {
            if (_plane.IsValid())
            {
                _renderer.material.color = ValidColor;
            }
            else
            {
                _renderer.material.color = InvalidColor;
            }
        }
    }

    private void UpdateRenderer()
    {
        if (_setupInProgress)
        {
            _renderer.material = SetupMaterial;
            _lineRenderer.material = SetupMaterial;
        }
        else
        {
            _renderer.material = GameMaterial;
            _lineRenderer.material = GameMaterial;
        }
    }
}