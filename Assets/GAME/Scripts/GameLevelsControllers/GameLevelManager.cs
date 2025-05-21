using System;
using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameLevelManager : MonoBehaviour
{
    public static GameLevelManager instance;

    public GameLevelData gameSelectionScene;
    public GameLevelData[] gameLevelsScenes;

    GameObject levelControllerInterface;

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

    public void LoadRandomGameLevelScene()
    {
        LoadGameLevelScene(gameLevelsScenes[UnityEngine.Random.Range(0,gameLevelsScenes.Length)]);
    }

    [ContextMenu("LoadGameLevelSelectorScene")]
    public void LoadGameLevelSelectorScene()
    {
        LoadGameLevelScene(gameSelectionScene);
    }
    public GameLevelData[] GetGameLevelsData()
    {
        return gameLevelsScenes;
    }

    public async void LoadGameLevelScene(GameLevelData gameLevelData)
    {
        await SceneManager.LoadSceneAsync(gameLevelData.scene.name);

        levelControllerInterface = FindObjectsByType<MonoBehaviour>(sortMode: FindObjectsSortMode.None)
            .FirstOrDefault(obj => obj is LevelControllerInterface)?.gameObject;

        levelControllerInterface.GetComponent<LevelControllerInterface>().StartLevel();
    }

    public void ResetGameLevelsData()
    {
        for (int i = 0; i < gameLevelsScenes.Length; i++)
        {
            gameLevelsScenes[i].timesPlayed = 0;
        }
    }

    public void CheckGameLevelFinished()
    {
        CharacterHealth[] playersOnScene = GameObject.FindObjectsByType<CharacterHealth>(FindObjectsSortMode.None);
        if(playersOnScene.Length <= 1)
        {
            levelControllerInterface.GetComponent<LevelControllerInterface>().FinishLevel();
        }

    }
}

[Serializable]
public struct GameLevelData
{
    public SceneAsset scene;
    public int timesPlayed;
}