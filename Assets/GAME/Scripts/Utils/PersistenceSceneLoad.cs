using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentSceneLoad : MonoBehaviour
{
    public string SceneName;
    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        if (!SceneManager.GetSceneByName(SceneName).isLoaded)
        {
            SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        }
    }
}