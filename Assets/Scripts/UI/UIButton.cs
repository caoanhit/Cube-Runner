using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : InterfaceElement
{
    void Start()
    {
        anim = GetComponent<Animation>();
    }
    public void Pressed()
    {
        if (!anim.isPlaying)
        {
            AnimationState state = anim[style.animation];
            state.speed = style.speed;
            anim.Play(style.animation);
        }
    }
    private void OnEnable()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
    }
}
