using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : InterfaceElement
{
    public void Pressed()
    {
        if (!anim.isPlaying)
        {
            AnimationState state = anim[style.buttonStyle.animation];
            state.speed = style.buttonStyle.speed;
            anim.Play(style.buttonStyle.animation);
        }
    }
    private void OnEnable()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
    }
}
