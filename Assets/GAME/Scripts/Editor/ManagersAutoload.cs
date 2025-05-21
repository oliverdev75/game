using UnityEngine;
using UnityEngine.SceneManagement;

public static class ManagersAutoload
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void LoadBootstrap()
    {
        if (!SceneManager.GetSceneByName("Managers").isLoaded)
        {
            SceneManager.LoadScene("Managers", LoadSceneMode.Additive);
        }
    }
}
