using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeTransition : MonoBehaviour
{
    [SerializeField] RectTransform maskRect;
    [SerializeField] float maskImageMaxSize = 2224f;
    [SerializeField] float maskImageMinSize = 0f;
    public Ease easeType = Ease.InOutExpo;

    void OnEnable()
    {
        maskRect.gameObject.SetActive(false);
    }

    [ContextMenu("FadeIn")]
    public void DebugFadeIn()
    {
        _ = FadeIn(1f);
    }

    [ContextMenu("FadeOut")]
    public void DebugFadeOut()
    {
        _ = FadeOut(1f);
    }

    public async Task FadeIn(float duration = 1f)
    {
        maskRect.gameObject.SetActive(true);
        maskRect.sizeDelta = new Vector2(maskImageMaxSize, maskImageMaxSize);

        Tween tween = maskRect
            .DOSizeDelta(new Vector2(maskImageMinSize, maskImageMinSize), duration)
            .SetEase(easeType);

        await tween.AsyncWaitForCompletion();
    }

    public async Task FadeOut(float duration = 1f)
    {
        maskRect.gameObject.SetActive(true);
        maskRect.sizeDelta = new Vector2(maskImageMinSize, maskImageMinSize);

        Tween tween = maskRect
            .DOSizeDelta(new Vector2(maskImageMaxSize, maskImageMaxSize), duration)
            .SetEase(easeType);

        await tween.AsyncWaitForCompletion();
    }
}