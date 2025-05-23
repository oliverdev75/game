using UnityEngine;

public class Arena_LevelController : MonoBehaviour, LevelControllerInterface
{
    public void FinishLevel()
    {
        Debug.Log("Finish game");
    }

    public void PauseLevel()
    {
        throw new System.NotImplementedException();
    }

    public void StartLevel()
    {
    }

}
