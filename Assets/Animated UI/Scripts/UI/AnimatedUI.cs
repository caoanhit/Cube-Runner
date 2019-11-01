using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
[RequireComponent(typeof(CanvasGroup))]
public class AnimatedUI : Animated
{
    public bool hideOnStart;
    public UIAnimation anim;
    protected UIData data;
    protected virtual void Awake()
    {
        data = new UIData();
        data.rect = GetComponent<RectTransform>();
        data.originalPos = data.rect.anchoredPosition;
        data.canvasGroup = GetComponent<CanvasGroup>();
        if (hideOnStart)
        {
            HideImmediate();
        }
        else
        {
            ShowImmediate();
        }
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
    public override void HideImmediate()
    {
        anim.HideImmediate(data);
        gameObject.SetActive(false);
    }
    public override void ShowImmediate()
    {
        gameObject.SetActive(true);
        anim.ShowImmediate(data);
    }
    public void AShow()
    {
        Show();
    }
    public void AHide()
    {
        Hide();
    }
}
