using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
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
            anim.SetTrigger("Check");
        }
    }
    public void Disable()
    {
        anim.Rebind();
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
    }
}
