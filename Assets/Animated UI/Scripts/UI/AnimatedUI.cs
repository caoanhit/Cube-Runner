using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
[RequireComponent(typeof(CanvasGroup))]
public class AnimatedUI : Animated
{
    public UIAnimation anim;
    protected UIData data;
    protected virtual void Start()
    {
        data = new UIData();
        data.rect = GetComponent<RectTransform>();
        data.originalPos = data.rect.anchoredPosition;
        data.canvasGroup = GetComponent<CanvasGroup>();
    }
    [ContextMenu("Show")]
    public override Sequence Show()
    {
        gameObject.SetActive(true);
        return anim.Show(data);
    }
    [ContextMenu("Hide")]
    public override Sequence Hide()
    {
        Sequence sequence = anim.Hide(data);
        sequence.OnComplete(() => gameObject.SetActive(false));
        return sequence;
    }
}
