using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : InterfaceElement
{
    public bool isUp;
    private void Awake()
    {
        if (gameObject.activeSelf) isUp = true;
        else isUp = false;
    }
    private void Update()
    {
        if (!isUp && !anim.isPlaying) gameObject.SetActive(false);
    }
    public void Open()
    {
        if (!isUp)
        {
            gameObject.SetActive(true);
            isUp = true;
            if (!anim.isPlaying)
            {
                AnimationState state = anim[style.menuStyle.animation];
                state.speed = style.menuStyle.speed;
                anim.Play(style.menuStyle.animation);
            }
        }
    }
    public void Close()
    {
        if (isUp)
        {
            if (!anim.isPlaying)
            {
                AnimationState state = anim[style.menuStyle.animation];
                state.speed = -style.menuStyle.speed;
                state.time = state.length;
                anim.Play(style.menuStyle.animation);
            }
            isUp = false;
        }
    }
}
