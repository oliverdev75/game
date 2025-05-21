using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] SceneAsset DEBUG_loadScene;

    async void Start()
    {
        await SceneManager.LoadSceneAsync("Managers", LoadSceneMode.Additive);

        if (!DEBUG_loadScene)
            SceneManager.LoadScene("MainMenu");
        else
            SceneManager.LoadScene(DEBUG_loadScene.name);

    }

}
