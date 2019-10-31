using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "UI Animation")]
public class UIAnimation : ScriptableObject
{
    public float duration = 0.5f;
    public AnimInfo showTarget;
    public AnimInfo hideTarget;
    public Sequence Hide(UIData data)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(DOTween.To(() => data.rect.localScale, x => data.rect.localScale = x, hideTarget.targetScale, duration).SetEase(hideTarget.scaleEase));
        sequence.Insert(0, DOTween.To(() => data.rect.anchoredPosition, x => data.rect.anchoredPosition = x, hideTarget.targetPos + data.originalPos, duration).SetEase(hideTarget.moveEase));
        sequence.Insert(duration * (1 - 1 / hideTarget.fadeSpeed), DOTween.To(() => data.canvasGroup.alpha, x => data.canvasGroup.alpha = x, hideTarget.targetAlpha, duration / hideTarget.fadeSpeed).SetEase(hideTarget.fadeEase));

        return sequence;
    }

    public Sequence Show(UIData data)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(DOTween.To(() => data.rect.localScale, x => data.rect.localScale = x, showTarget.targetScale, duration).SetEase(showTarget.scaleEase));
        sequence.Insert(0, DOTween.To(() => data.rect.anchoredPosition, x => data.rect.anchoredPosition = x, showTarget.targetPos + data.originalPos, duration).SetEase(showTarget.moveEase));
        sequence.Insert(duration * (1 - 1 / showTarget.fadeSpeed), DOTween.To(() => data.canvasGroup.alpha, x => data.canvasGroup.alpha = x, showTarget.targetAlpha, duration / showTarget.fadeSpeed).SetEase(showTarget.fadeEase));

        return sequence;
    }
}

[System.Serializable]
public class AnimInfo
{
    public Vector2 targetPos;
    public Ease moveEase = Ease.Linear;
    public Vector3 targetScale = Vector3.one;
    public Ease scaleEase = Ease.Linear;
    [Range(0, 1)]
    public float targetAlpha = 1;
    public Ease fadeEase = Ease.Linear;
    [Min(1)]
    public float fadeSpeed = 1;
}

[System.Serializable]
public class UIData
{
    public RectTransform rect;
    public Vector2 originalPos;
    public CanvasGroup canvasGroup;
}
