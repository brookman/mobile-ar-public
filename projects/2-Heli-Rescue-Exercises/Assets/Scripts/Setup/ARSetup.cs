using UnityEngine;
using UnityEngine.SceneManagement;

public class ARSetup : MonoBehaviour
{
    private const string SCENE_NAME = "ARSetup";
    private static bool _loaded;

    private void Awake()
    {
        if (!_loaded)
        {
            _loaded = true;
            SceneManager.LoadScene(SCENE_NAME, LoadSceneMode.Additive);
        }

        DestroyImmediate(gameObject);
    }

    public static void Reset()
    {
        _loaded = false;
    }
}