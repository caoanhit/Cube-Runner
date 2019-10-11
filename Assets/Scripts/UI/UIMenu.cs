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
    private void Start()
    {
        anim = GetComponent<Animation>();
        anim.AddClip(style.openAnim.anim, "Open");
        anim.AddClip(style.closeAnim.anim, "Close");
        if (!isUp) gameObject.SetActive(false);
    }
    private void Update()
    {
        if (!isUp && !anim.isPlaying)
        {
            OnClosed?.Invoke();
            gameObject.SetActive(false);
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
            if (!anim.isPlaying)
            {
                AnimationState state = anim["Open"];
                state.speed = style.openAnim.speed;
                anim.Play("Open");
            }
        }
    }
    public void Close()
    {
        if (isUp)
        {
            if (!anim.isPlaying)
            {
                AnimationState state = anim["Close"];
                state.speed = style.closeAnim.speed;
                anim.Play("Close");
            }
            isUp = false;
        }
    }
    public void CloseImediate()
    {
        isUp = false;
        gameObject.SetActive(false);
    }
}
