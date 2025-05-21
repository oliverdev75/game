using UnityEngine;
using TMPro;
using System;

public class TimerDisplay : MonoBehaviour
{
    public TextMeshProUGUI timer_text;
    public Gradient timerText_gradient;
    Timer timer;

    void Update()
    {
        if (timer == null)
            return;

        TimeSpan time = TimeSpan.FromSeconds(timer.TimeElapsed);
        int centiseconds = time.Milliseconds / 10;
        timer_text.text = string.Format("{0:D2}:{1:D2}", time.Seconds, centiseconds);
        timer_text.color = timerText_gradient.Evaluate(timer.GetElapsedTimeNormalized());
    }

    public void SetTimer(Timer timer)
    {
        this.timer = timer;
    }
}
