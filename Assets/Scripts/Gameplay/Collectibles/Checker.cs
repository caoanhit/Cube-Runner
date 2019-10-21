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
    private void Start()
    {
    }
    public void Check(Vector3 pos)
    {
        if (Vector3.Distance(pos, transform.position + (transform.forward + Vector3.up) * 0.5f) < precision)
        {
            OnPerfect.Invoke(id);
            ParticleManager.instance.Play("Checker", gameObject.transform.position + gameObject.transform.forward * 0.5f + Vector3.up * 0.51f);
            gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        id = currentid;
        currentid++;
    }
}
