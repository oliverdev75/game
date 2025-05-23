using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Timer
{
    public float TimeElapsed { get; private set; }
    public bool IsRunning { get; private set; }

    private float TargetTime = 0f;
    public Action OnTimerFinished;

    public float GetElapsedTimeNormalized()
    {
        if (TargetTime <= 0f) return 0f;    // Dividir entre 0 peta

        return Mathf.Clamp01(TimeElapsed / TargetTime);
    }


    public void Start(float targetTime)
    {
        TimeElapsed = 0f;
        TargetTime = targetTime;
        IsRunning = true;
    }

    public void Stop()
    {
        IsRunning = false;
    }

    public void Reset(float targetTime = 0)
    {
        if(targetTime != 0)
            TargetTime = targetTime;

        TimeElapsed = 0f;
    }

    public void Update(float deltaTime)
    {
        if (IsRunning)
        {
            if (TimeElapsed > TargetTime)
            {
                OnTimerFinished?.Invoke();
                TargetTime = TimeElapsed;
                Stop();
            }

            TimeElapsed += deltaTime;
        }
    }
}
