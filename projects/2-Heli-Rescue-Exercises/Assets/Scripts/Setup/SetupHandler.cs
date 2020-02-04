using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class SetupHandler : MonoBehaviour
{
    private const string NextScene = "2_Game";

    public Button ConfirmButton;
    public TMP_Text InfoText;
    public RayCastVisualizer RayCastVisualizer;

    private ARPlaneManager _planeManager;
    private bool _hasValidPlane;

    void Start()
    {
        _planeManager = FindObjectOfType<ARPlaneManager>();
        CustomARPlane.SetupInProgress = true;
        RayCastVisualizer.enabled = true;

        ConfirmButton.onClick.AddListener(() =>
        {
            if (_hasValidPlane && RayCastVisualizer.Pose.HasValue)
            {
                RayCastVisualizer.enabled = false;
                ConfirmButton.gameObject.SetActive(false);
                CustomARPlane.SetupInProgress = false;

                var pose = RayCastVisualizer.Pose.Value;
                Transform poseTransform;
                if (Application.isEditor)
                {
                    var editorAnchor = new GameObject("[EditorAnchor]");
                    editorAnchor.AddComponent<DontDestroyOnLoad>();
                    editorAnchor.transform.position = pose.position;
                    editorAnchor.transform.rotation = pose.rotation;
                    poseTransform = editorAnchor.transform;
                }
                else
                {
                    var anchor = FindObjectOfType<ARAnchorManager>().AddAnchor(pose);
                    anchor.gameObject.AddComponent<DontDestroyOnLoad>();
                    poseTransform = anchor.transform;
                }

                Buildings.AnchorTransform = poseTransform;
                SceneManager.LoadScene(NextScene);
            }
        });

        const string msg = "Please scan a large enough horizontal plane for the play area";

        if (Application.isEditor)
        {
            InfoText.text = $"DEV - {msg}. 2s left!";
            StartCoroutine(TriggerDelayed(2f, OnPlaneFound));
        }
        else
        {
            InfoText.text = msg;
            StartCoroutine(TriggerDelayed(2f, () => { _planeManager.planesChanged += PlaneManagerOnPlanesChanged; }));
        }
    }

    private void Update()
    {
        if (_hasValidPlane && RayCastVisualizer.Pose.HasValue)
        {
            ConfirmButton.gameObject.SetActive(true);
        }
        else
        {
            ConfirmButton.gameObject.SetActive(false);
        }

        if (_hasValidPlane)
        {
            if (RayCastVisualizer.Pose.HasValue)
            {
                InfoText.text = "";
            }
            else
            {
                InfoText.text = "Look at a large enough horizontal plane";
            }
        }
    }

    private void OnPlaneFound()
    {
        _planeManager.planesChanged -= PlaneManagerOnPlanesChanged;
        _hasValidPlane = true;
        InfoText.text = "";
    }

    private static IEnumerator TriggerDelayed(float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        callback.Invoke();
    }

    private void PlaneManagerOnPlanesChanged(ARPlanesChangedEventArgs eventArgs)
    {
        if (UpdatePlaneStatus(eventArgs.added) || UpdatePlaneStatus(eventArgs.updated))
        {
            OnPlaneFound();
        }
    }

    private static bool UpdatePlaneStatus(IEnumerable<ARPlane> planes)
    {
        return planes.Any(p => p.IsValid());
    }
}