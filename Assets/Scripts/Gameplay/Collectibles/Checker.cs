using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Checker : MonoBehaviour
{
    public static event Action<int> OnPerfect;
    public static event Action<Vector3> OnPerfectPos;

    static int currentid;
    int id;
    const float precision = 0.2f;
    private void Start()
    {
    }
    public void Check(Vector3 pos)
    {
        if (Vector3.Distance(pos, transform.position + (transform.forward + Vector3.up) * 0.5f) < precision)
        {
            OnPerfect.Invoke(id);
            OnPerfectPos.Invoke(transform.position);
            ParticleManager.instance.Play("Checker", gameObject.transform.position + gameObject.transform.forward * 0.5f + Vector3.up * 0.51f);
            gameObject.SetActive(false);
        }
    }
    public void GetId()
    {
        id = currentid;
        currentid++;
    }
}
