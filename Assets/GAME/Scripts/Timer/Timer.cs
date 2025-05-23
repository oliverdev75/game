using System;
using UnityEngine;

[Serializable]
public class Timer
{
    public float TimeElapsed { get; private set; }
    public bool IsRunning { get; private set; }

    private float TargetTime = 0f;
    private bool isCountdown = false;
    public Action OnTimerFinished;

    public float GetElapsedTimeNormalized()
    {
        if (TargetTime <= 0f) return 0f;
        return Mathf.Clamp01(TimeElapsed / TargetTime);
    }

    public float GetRemainingTime()
    {
        return Mathf.Max(0f, TargetTime - TimeElapsed);
    }

    public void Start(float targetTime, bool countdown = false)
    {
        TargetTime = targetTime;
        isCountdown = countdown;

        if (isCountdown)
            TimeElapsed = targetTime;
        else
            TimeElapsed = 0f;

        IsRunning = true;
    }

    public void Stop()
    {
        IsRunning = false;
    }

    public void Reset(float targetTime = 0)
    {
        if (targetTime > 0)
            TargetTime = targetTime;

        TimeElapsed = isCountdown ? TargetTime : 0f;
    }

    public void Update(float deltaTime)
    {
        if (!IsRunning) return;

        if (isCountdown)
        {
            TimeElapsed -= deltaTime;

            if (TimeElapsed <= 0f)
            {
                TimeElapsed = 0f;
                OnTimerFinished?.Invoke();
                Stop();
            }
        }
        else
        {
            TimeElapsed += deltaTime;

            if (TimeElapsed >= TargetTime)
            {
                TimeElapsed = TargetTime;
                OnTimerFinished?.Invoke();
                Stop();
            }
        }
    }
}