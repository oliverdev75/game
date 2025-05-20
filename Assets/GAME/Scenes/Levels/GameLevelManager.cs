using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLevelManager : MonoBehaviour
{
    public static GameLevelManager instance;

    public GameLevelData GameSelectionScene;
    public GameLevelData[] GameLevelsScenesNames;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadGameLevelScene(GameLevelData gameLevelData)
    {
        SceneManager.LoadScene(gameLevelData.SceneName);
    }
}

[Serializable]
public struct GameLevelData
{
    public string SceneName;
    private int timesPlayed;
}