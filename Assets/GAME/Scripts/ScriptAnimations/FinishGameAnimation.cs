using System;
using System.Collections;
using UnityEngine;

public class FinishGameAnimation : MonoBehaviour
{

    public Action OnAnimationFinished;
    public Camera camera;

    Coroutine coroutine;

    private void OnEnable()
    {
        PlayAnimation();
    }

    public void PlayAnimation()
    {
        if(coroutine == null)
        {
            coroutine = StartCoroutine(FinishGameAnimationCorutine());
        }
    }

    IEnumerator FinishGameAnimationCorutine()
    {
        Time.timeScale = 0.1f;

        camera.enabled = true;

        Vector3 playerPos = GameObject.FindFirstObjectByType<CharacterHealth>(FindObjectsInactive.Exclude).transform.position;
        if (playerPos == null)
            playerPos = GameObject.FindFirstObjectByType<CharacterHealth>(FindObjectsInactive.Include).transform.position;

        transform.position = playerPos;

        yield return new WaitForSecondsRealtime(3f);

        camera.enabled = false;
        coroutine = null;

        OnAnimationFinished?.Invoke();

        Time.timeScale = 1.0f;
    }
}
