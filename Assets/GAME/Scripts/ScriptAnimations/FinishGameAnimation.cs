using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class FinishGameAnimation : MonoBehaviour
{
    public Action OnAnimationFinished;
    public Camera camera;

    Coroutine coroutine;

    Vector3 originalPosition;
    float originalOrthoSize;

    private void OnEnable()
    {
        originalPosition = transform.position;
        originalOrthoSize = camera.orthographicSize;
        PlayAnimation();
    }

    public void PlayAnimation()
    {
        if (coroutine == null)
        {
            coroutine = StartCoroutine(FinishGameAnimationCoroutine());
        }
    }

    IEnumerator FinishGameAnimationCoroutine()
    {
        const float TARGET_ORT_SIZE = 3f;
        const float FOLLOW_DURATION = 5f;
        const float ZOOM_DURATION = 0.5f;
        const float RETURN_DURATION = 1f;

        camera.enabled = true;

        var player = GameObject.FindFirstObjectByType<CharacterHealth>(FindObjectsInactive.Exclude);
        if (player == null)
            yield break;
        
        yield return new WaitForSeconds(0.5f);

        Transform lastPlayerDeadTransform = GameObject.FindFirstObjectByType<PlayerSpawnManager>().GetLastPlayerDeath().transform;
        Transform playerTransform = player.transform;

        // Zoom in
        Tween zoomIn = camera.DOOrthoSize(TARGET_ORT_SIZE, ZOOM_DURATION).SetEase(Ease.InOutExpo).SetUpdate(true);
        Tween moveIn = transform.DOMove(lastPlayerDeadTransform.position, RETURN_DURATION).SetEase(Ease.InOutExpo).SetUpdate(true);

        yield return zoomIn.WaitForCompletion();
        yield return moveIn.WaitForCompletion();

        float elapsed = 0f;
        while (elapsed < FOLLOW_DURATION)
        {
            transform.position = Vector3.Lerp(transform.position, playerTransform.position, Time.unscaledDeltaTime * 5f);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        // Zoom out y volver a la posición original
        Tween zoomOut = camera.DOOrthoSize(originalOrthoSize, RETURN_DURATION).SetEase(Ease.InOutSine).SetUpdate(true);
        Tween moveBack = transform.DOMove(originalPosition, RETURN_DURATION).SetEase(Ease.InOutSine).SetUpdate(true);

        yield return zoomOut.WaitForCompletion();
        yield return moveBack.WaitForCompletion();

        camera.enabled = false;
        coroutine = null;
        OnAnimationFinished?.Invoke();
    }
}
