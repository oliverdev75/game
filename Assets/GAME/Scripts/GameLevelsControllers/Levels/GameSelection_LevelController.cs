using UnityEngine;

public class GameSelection_LevelController : MonoBehaviour, LevelControllerInterface
{
    [Header("Game Selection Scenes")]
    public GameLevelData gameLevelData1;
    public GameLevelData gameLevelData2;

    [Header("Game Selection Paramters")]
    public Vector2 levelOption1Position;
    public Vector2 levelOption2Position;
    public Vector2 zoneSize = Vector2.one;
    public LayerMask playerLayer;

    Timer timer;


    [ContextMenu("Finish")]
    public void FinishLevel()
    {
        RaycastHit2D[] hits_zone1 = Physics2D.BoxCastAll(levelOption1Position,zoneSize,0,Vector2.up,zoneSize.magnitude,playerLayer);
        RaycastHit2D[] hits_zone2 = Physics2D.BoxCastAll(levelOption2Position,zoneSize,0,Vector2.up,zoneSize.magnitude,playerLayer);

        // Si nadie vota empieza un nivel aleatorio
        if (hits_zone2.Length + hits_zone1.Length == 0)
            GameLevelManager.instance.LoadGameLevelScene(Random.Range(0, 1) == 0 ? gameLevelData1 : gameLevelData2);

        // Empieza el nivel mas votado
        if (hits_zone1.Length > hits_zone2.Length)
            GameLevelManager.instance.LoadGameLevelScene(gameLevelData1);
        else
            GameLevelManager.instance.LoadGameLevelScene(gameLevelData2);
    }

    public void PauseLevel()
    {
    }

    public void StartLevel()
    {
        timer = new Timer();
        timer.Start(15f);   // 30 segundos para que el nivel acabe
        timer.OnTimerFinished += FinishLevel;

        GameLevelData[] allGameLevelsData = GameLevelManager.instance.GetGameLevelsData();
        gameLevelData1 = allGameLevelsData[0];
        gameLevelData2 = allGameLevelsData[1];
    }

    void Update()
    {
        if (timer != null)
        {
            timer.Update(Time.deltaTime);
            Debug.Log(timer.GetElapsedTimeNormalized());
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(levelOption1Position, zoneSize);
        Gizmos.DrawWireCube(levelOption2Position, zoneSize);
    }

}
