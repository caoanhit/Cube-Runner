using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(CanvasGroup))]
public class InterfaceElement : MonoBehaviour
{
    public UIStyleData style;
    protected Animation anim;
    void Start()
    {
        anim = GetComponent<Animation>();
    }
#if UNITY_EDITOR
    private void Reset()
    {
        Animation a = GetComponent<Animation>();
        Object[] clips = AssetDatabase.LoadAllAssetsAtPath("Assets/Animation/UI/");
        foreach (Object clip in clips)
        {
            AnimationClip animation = (AnimationClip)clip;
            a.AddClip(animation, animation.name);
        }
    }
#endif
}
