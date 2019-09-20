using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainButtonController : MonoBehaviour
{
    public void BackToMain()
    {
        foreach (var obj in FindObjectsOfType<DontDestroyOnLoad>())
        {
            obj.DontDestroyOnLoadEnabled = false;
        }

        ARSetup.Reset();
        CameraCache.Reset();
        SceneManager.LoadScene("0_MainMenu");
    }
}