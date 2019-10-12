using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpdateType
{
    Update,
    FixedUpdate
}
public class CameraControl : MonoBehaviour
{
    public UpdateType updateType;
    [Range(0, 1)]
    public float smoothness;
    public Vector3 charSelectOffset;
    private Transform target;
    private Vector3 Offset;
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
            transform.position = Vector3.SmoothDamp(transform.position, target.position + Offset, ref vel, smoothness, Mathf.Infinity, Time.fixedDeltaTime);
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
    public void EnterCharSelect()
    {
        Offset = charSelectOffset;
    }
    public void ExitCharSelect()
    {
        Offset = Vector3.zero;
    }
}
