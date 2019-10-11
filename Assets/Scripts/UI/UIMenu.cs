using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIMenu : InterfaceElement
{
    public bool isUp;

    public UnityEvent OnAnimationFinish;
    bool finished;
    private void Start()
    {
        anim = GetComponent<Animation>();
        if (!isUp) gameObject.SetActive(false);
    }
    private void Update()
    {
        if (!isUp && !anim.isPlaying) gameObject.SetActive(false);
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
                AnimationState state = anim[style.animation];
                state.speed = style.speed;
                anim.Play(style.animation);
            }
        }
    }
    public void Close()
    {
        if (isUp)
        {
            if (!anim.isPlaying)
            {
                AnimationState state = anim[style.animation];
                state.speed = -style.speed;
                state.time = state.length;
                anim.Play(style.animation);
            }
            isUp = false;
        }
    }
}
