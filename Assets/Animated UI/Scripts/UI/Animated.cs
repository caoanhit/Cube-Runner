using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class Animated : MonoBehaviour
{
    public abstract Sequence Show();
    public abstract Sequence Hide();
    public abstract void HideImmediate();
    public abstract void ShowImmediate();
}
