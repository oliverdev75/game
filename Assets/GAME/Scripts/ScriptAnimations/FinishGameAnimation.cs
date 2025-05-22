using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
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
        const float TARGET_ORT_SIZE = 3f;
        const float FOLLOW_DURATION = 5f;
        const float ZOOM_DURATION = 0.5f;

        camera.enabled = true;

        Transform playerTransform = GameObject.FindFirstObjectByType<CharacterHealth>(FindObjectsInactive.Exclude).transform;

        if (playerTransform == null)
            yield break;

        // Zoom out
        Tween zoomTween = camera.DOOrthoSize(TARGET_ORT_SIZE, ZOOM_DURATION).SetEase(Ease.InOutSine).SetUpdate(true);

        float elapsed = 0f;

        while (elapsed < FOLLOW_DURATION)
        {
            transform.position = Vector3.Lerp(transform.position, playerTransform.position, Time.unscaledDeltaTime * 5f);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        camera.enabled = false;
        coroutine = null;
        OnAnimationFinished?.Invoke();
    }

}
