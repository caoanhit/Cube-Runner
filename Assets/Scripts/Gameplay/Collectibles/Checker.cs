using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Checker : MonoBehaviour
{
    public static event Action<int> OnPerfect;

    static int currentid;
    int id;
    const float precision = 0.2f;
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Check(Vector3 pos)
    {
        if (Vector3.Distance(pos, transform.position + (transform.forward + Vector3.up) * 0.5f) < precision)
        {
            OnPerfect.Invoke(id);
            anim.SetTrigger("Check");
        }
    }
    private void OnEnable()
    {
        id = currentid;
        currentid++;
    }
    public void Disable()
    {
        anim.Rebind();
        gameObject.SetActive(false);
    }
}
