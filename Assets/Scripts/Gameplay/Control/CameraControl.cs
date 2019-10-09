using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public enum UpdateType
    {
        Update,
        FixedUpdate
    }
    public UpdateType updateType;
    [Range(0, 1)]
    public float smoothness;
    private Transform target;
    Transform pivot;
    Vector3 vel;
    void Awake()
    {
        pivot = GetComponentsInChildren<Transform>()[1];
    }
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = target.position;
    }
    private void FixedUpdate()
    {
        if (updateType == UpdateType.FixedUpdate)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref vel, smoothness, Mathf.Infinity, Time.fixedDeltaTime);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (updateType == UpdateType.Update)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref vel, smoothness);
        }
    }
}
