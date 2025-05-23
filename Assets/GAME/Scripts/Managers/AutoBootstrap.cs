using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)]
public class AutoBootstrap : MonoBehaviour
{
    private async void Awake()
    {
        if (GameLevelManager.instance == null)
        {
            Debug.LogWarning("GameLevelManager not found, loading Managers scene...");
            await SceneManager.LoadSceneAsync("Managers", LoadSceneMode.Additive);
            GameObject.FindFirstObjectByType<GameLevelManager>().DEBUG_ReloadCurrentScene();
        }
    }
}