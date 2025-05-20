using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] string DEBUG_loadSceneName;

    void Start()
    {
        if (string.IsNullOrEmpty(DEBUG_loadSceneName))
            SceneManager.LoadScene("MainMenu");
        else
            SceneManager.LoadScene(DEBUG_loadSceneName);

    }

}
