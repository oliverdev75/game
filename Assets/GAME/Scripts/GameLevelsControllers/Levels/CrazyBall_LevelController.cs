using UnityEngine;

public class CrazyBall_LevelController : MonoBehaviour, LevelControllerInterface
{
    public Transform crazyBallTransform;
    
    public void StartLevel()
    {
        // Se ejecuta cuando la cuenta regresiva se acaba
    }
    
    public void FinishLevel()
    {
        // Se ejecuta cuando hay 1 player vivo (ultimo en pie)

    }

    public void PauseLevel()
    {
        throw new System.NotImplementedException();
    }

    public void ComproveLevelFinished()
    {
        throw new System.NotImplementedException();
    }
}
