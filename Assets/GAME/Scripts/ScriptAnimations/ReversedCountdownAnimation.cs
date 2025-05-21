using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class ReversedCountdownAnimation : MonoBehaviour
{
    public TextMeshProUGUI countdown_text;
    Coroutine animationCorutine = null;

    public Action onFinishAnimation;

    private void OnEnable()
    {
        PlayAnimation();
    }

    public void PlayAnimation()
    {
        if (animationCorutine == null)
            animationCorutine = StartCoroutine(ReversedCountdownAnimationCorutine());
    }

    IEnumerator ReversedCountdownAnimationCorutine()
    {
        const float TIME_WAIT = 0.6f;

        GameManager.instance.inputsAreEnabled = false;

        countdown_text.text = "3";
        yield return new WaitForSeconds(TIME_WAIT);
        countdown_text.text = "2";
        yield return new WaitForSeconds(TIME_WAIT);
        countdown_text.text = "1";
        yield return new WaitForSeconds(TIME_WAIT);
        countdown_text.text = "Dopamine!";
        yield return new WaitForSeconds(TIME_WAIT);

        GameManager.instance.inputsAreEnabled = true;

        onFinishAnimation?.Invoke();
        animationCorutine = null;
        gameObject.SetActive(false);
    }

}
