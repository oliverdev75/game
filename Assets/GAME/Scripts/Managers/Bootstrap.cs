using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] string sceneToLoad = "MainMenu";

    async void Start()
    {
        await SceneManager.LoadSceneAsync("Managers", LoadSceneMode.Additive);
        await SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);
    }
}