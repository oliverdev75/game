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

    [Header("Animations")]
    public GameObject finishGameAnimation;
    public GameObject reversedCountdownAnimation;

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

    [ContextMenu("DEBUG ReloadCurrentScene")]
    public void DEBUG_ReloadCurrentScene()
    {
        string currentActiveSceneName = SceneManager.GetActiveScene().name;

        foreach (var scene in gameLevelsScenes) 
        {
            if (scene.scene.name == currentActiveSceneName)
                LoadGameLevelScene(scene);
        }
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

        if(gameLevelData.scene.name == gameSelectionScene.scene.name)
        {
            levelControllerInterface.GetComponent<LevelControllerInterface>().StartLevel();
        }
        else
        {
            ReversedCountdownAnimation animation = Instantiate(reversedCountdownAnimation).GetComponent<ReversedCountdownAnimation>();
            animation.PlayAnimation();
            animation.onFinishAnimation += () =>
            {
                levelControllerInterface.GetComponent<LevelControllerInterface>().StartLevel();
            };
        }


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
            FinishGameAnimation finishGameAnim = Instantiate(finishGameAnimation).GetComponent<FinishGameAnimation>();
            finishGameAnim.PlayAnimation();
            finishGameAnim.OnAnimationFinished += () =>
            {
                levelControllerInterface.GetComponent<LevelControllerInterface>().FinishLevel();
                LoadGameLevelSelectorScene();
            };
        }

    }
}

[Serializable]
public struct GameLevelData
{
    public SceneAsset scene;
    public int timesPlayed;
    public Sprite thumbnail;
}