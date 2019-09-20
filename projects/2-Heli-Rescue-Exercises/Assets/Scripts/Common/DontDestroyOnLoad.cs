using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(DontDestroyOnLoad))]
public class DontDestroyOnLoadEditor : UnityEditor.Editor
{
    private DontDestroyOnLoad _script => target as DontDestroyOnLoad;

    public override void OnInspectorGUI()
    {
        _script.DontDestroyOnLoadEnabled = UnityEditor.EditorGUILayout.Toggle("DontDestroyOnLoad", _script.DontDestroyOnLoadEnabled);
    }
}
#endif

public class DontDestroyOnLoad : MonoBehaviour
{
    [SerializeField]
    private bool _dontDestroyOnLoad = true;

    public bool DontDestroyOnLoadEnabled
    {
        get => _dontDestroyOnLoad;
        set
        {
            if (value == _dontDestroyOnLoad) return;
            _dontDestroyOnLoad = value;
            UpdateDontDestroyOnLoadState();
        }
    }

    private void Awake()
    {
        UpdateDontDestroyOnLoadState();
    }

    private void UpdateDontDestroyOnLoadState()
    {
        if (_dontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
        }
    }
}