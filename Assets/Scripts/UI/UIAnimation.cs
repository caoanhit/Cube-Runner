using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class UIAnimation : MonoBehaviour
{
    public bool OpenOnAwake;
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        if (OpenOnAwake) anim.SetBool("isOpen", true);
        else
        {
            anim.SetBool("isOpen", false);
            gameObject.SetActive(false);
        }
    }
    public void Open()
    {
        anim.SetBool("isOpen", true);
    }
    public void Close()
    {
        anim.SetBool("isOpen", true);
    }
}
