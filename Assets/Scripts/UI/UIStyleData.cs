using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UIStyleData : ScriptableObject
{
    public UIAnimationStyle menuStyle;
    public UIAnimationStyle buttonStyle;
}
[System.Serializable]
public class UIAnimationStyle
{
    public string animation;
    public float speed;
}