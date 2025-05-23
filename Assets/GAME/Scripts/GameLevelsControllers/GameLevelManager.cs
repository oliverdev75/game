using System;
using System.Linq;
using System.Collections;
using System.Threading.Tasks;
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
    public GameObject fadeTransitionAnimation;

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

#if UNITY_EDITOR
    private void OnValidate()
    {
        for (int i = 0; i < gameLevelsScenes.Length; i++)
        {
            var level = gameLevelsScenes[i];
            level.SyncSceneName();
            gameLevelsScenes[i] = level;
        }

        var selection = gameSelectionScene;
        selection.SyncSceneName();
        gameSelectionScene = selection;

        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif
    
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
            if (scene.sceneName == currentActiveSceneName)
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
        await InstantiateTransition(easeIn: false);
        
        await SceneManager.LoadSceneAsync(gameLevelData.sceneName);
        
        SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);

        levelControllerInterface = FindObjectsByType<MonoBehaviour>(sortMode: FindObjectsSortMode.None)
            .FirstOrDefault(obj => obj is LevelControllerInterface)?.gameObject;
        
        // Start level controller interface
        if(gameLevelData.sceneName == gameSelectionScene.sceneName)
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
        
        await InstantiateTransition(easeIn: true);

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

            FinishGameAnimation finishGameAnim = Instantiate(finishGameAnimation).GetComponent<FinishGameAnimation>();
            finishGameAnim.PlayAnimation();
            finishGameAnim.OnAnimationFinished += () =>
            {
                LoadGameLevelSelectorScene();
            };
        }
    }

    public Task InstantiateTransition(bool easeIn)
    {
        FadeTransition transition = Instantiate(fadeTransitionAnimation).GetComponent<FadeTransition>();
        Destroy(transition.gameObject, 3f);

        if (easeIn)
        {
            Vector2 center = Vector2.zero;
            if(GameObject.Find("SpawnPoint"))
                Camera.main.WorldToScreenPoint(GameObject.Find("SpawnPoint").transform.position);
            
            return transition.FadeIn(1f);
        }
        else
        {
            return transition.FadeOut(1f);
        }
    }
    
}

[Serializable]
public struct GameLevelData
{
#if UNITY_EDITOR
    public SceneAsset sceneAsset;
#endif
    public string sceneName;
    public int timesPlayed;
    public Sprite thumbnail;

#if UNITY_EDITOR
    public void SyncSceneName()
    {
        if (sceneAsset != null)
        {
            string path = AssetDatabase.GetAssetPath(sceneAsset);
            sceneName = System.IO.Path.GetFileNameWithoutExtension(path);
        }
    }
#endif
}
