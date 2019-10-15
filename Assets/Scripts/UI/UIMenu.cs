using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(CanvasGroup))]
public class UIMenu : MonoBehaviour
{
    public UIAnimationStyle style;
    public bool isUp;

    public UnityEvent OnAnimationFinish;
    public UnityEvent OnClosed;
    bool finished;
    Animation anim;
    CanvasGroup canvasGroup;
    RectTransform rectTransform;
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        anim = GetComponent<Animation>();
        anim.AddClip(style.openAnim.anim, "Open");
        anim.AddClip(style.closeAnim.anim, "Close");
        if (!isUp) gameObject.SetActive(false);
    }
    private void Update()
    {
        if (!isUp && !anim.isPlaying)
        {
            gameObject.SetActive(false);
            OnClosed?.Invoke();
        }
        if (isUp && !finished && !anim.isPlaying)
        {
            finished = true;
            OnAnimationFinish?.Invoke();
        }
    }
    public void Open()
    {
        if (!isUp)
        {
            finished = false;
            gameObject.SetActive(true);
            isUp = true;
            AnimationState state = anim["Open"];
            state.speed = style.openAnim.speed;
            if (style.openAnim.speed < 0) state.time = state.length;
            else state.time = 0;
            anim.Play("Open");
        }
    }
    public void Close()
    {
        if (isUp)
        {

            AnimationState state = anim["Close"];
            state.speed = style.closeAnim.speed;
            if (style.closeAnim.speed < 0) state.time = state.length;
            anim.Play("Close");
            isUp = false;
        }
    }
    public void CloseImmediate()
    {
        isUp = false;
        gameObject.SetActive(false);
    }
    public void OpenImmediate()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 1;
        rectTransform.localScale = Vector3.one;
        isUp = true;
    }
}
