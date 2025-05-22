using UnityEngine;

public class GameSelection_LevelController : MonoBehaviour, LevelControllerInterface
{

    [Header("Game Selection Paramters")]
    public Vector2 levelOption1Position;
    public Vector2 levelOption2Position;
    public Vector2 zoneSize = Vector2.one;
    public LayerMask playerLayer;

    [Header("Game Thumbnails")]
    public SpriteRenderer levelOption1Thumbnail;
    public SpriteRenderer levelOption2Thumbnail;

    [Header("Timer Display")]
    public TimerDisplay timerDisplay;

    Timer timer;
    GameLevelData gameLevelData1;
    GameLevelData gameLevelData2;


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
        timer.Start(10f);   // 30 segundos para que el nivel acabe
        timer.OnTimerFinished += FinishLevel;
        timerDisplay.SetTimer(timer);

        GameLevelData[] allGameLevelsData = GameLevelManager.instance.GetGameLevelsData();
        int level1Index = Random.Range(0, allGameLevelsData.Length);
        int level2Index = Random.Range(0, allGameLevelsData.Length);

        if (level1Index == level2Index)
            level2Index = (level2Index + 1) % allGameLevelsData.Length;

        gameLevelData1 = allGameLevelsData[level1Index];
        gameLevelData2 = allGameLevelsData[level2Index];

        levelOption1Thumbnail.sprite = gameLevelData1.thumbnail;
        levelOption2Thumbnail.sprite = gameLevelData2.thumbnail;
    }

    void Update()
    {
        if (timer != null)
        {
            timer.Update(Time.deltaTime);
        }

        RaycastHit2D[] hits_zone1 = Physics2D.BoxCastAll(levelOption1Position, zoneSize, 0, Vector2.up, zoneSize.magnitude, playerLayer);
        RaycastHit2D[] hits_zone2 = Physics2D.BoxCastAll(levelOption2Position, zoneSize, 0, Vector2.up, zoneSize.magnitude, playerLayer);

        if (hits_zone1.Length + hits_zone2.Length == GameManager.instance.numberOfPlayers && timer.IsRunning)
        {
            timer.Stop();
            FinishLevel();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(levelOption1Position, zoneSize);
        Gizmos.DrawWireCube(levelOption2Position, zoneSize);
    }

}
