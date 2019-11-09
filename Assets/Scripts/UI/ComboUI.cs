using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Animator))]
public class ComboUI : MonoBehaviour
{
    public Text text;

    Animator anim;
    RectTransform rect;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rect = GetComponent<RectTransform>();
    }
    public void Show(Vector3 pos, Quaternion rotation, int value)
    {
        gameObject.SetActive(true);
        anim.SetTrigger("Show");
        text.text = "+" + value.ToString();
        rect.anchoredPosition3D = pos;
        rect.rotation = rotation;
    }
}