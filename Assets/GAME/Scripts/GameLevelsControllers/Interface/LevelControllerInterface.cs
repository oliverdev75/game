using UnityEngine;

public interface LevelControllerInterface
{
    void Start()
    {
        if (GameLevelManager.instance == null)
        {
            Debug.LogWarning("GameLevelManager no está presente. La escena fue cargada directamente.");
            StartLevel();
        }
    }

    public void StartLevel();
    public void PauseLevel();
    public void FinishLevel();
}
