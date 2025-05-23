using System;
using UnityEngine;

public class CrazyBall_LevelController : MonoBehaviour, LevelControllerInterface
{
    public CrazyBallManager crazyBall;

    private void Awake()
    {
        crazyBall.enabled = false;
    }

    public void StartLevel()
    {
        crazyBall.enabled = true;
    }
    
    public void FinishLevel()
    {
        crazyBall.enabled = false;
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
