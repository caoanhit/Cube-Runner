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
    public Vector3 worldSelectOffset;
    [Range(0, 1)]
    public float snapSpeed;
    private Transform target;
    private Vector3 Offset;
    private Vector3 targetOffset;
    private Camera cam;
    Transform pivot;
    Vector3 vel;
    Vector3 vel1;
    Vector3 targetpos;
    float fov;
    float zoom = 1;
    void Awake()
    {
        pivot = GetComponentsInChildren<Transform>()[1];
        cam = GetComponentInChildren<Camera>();
        fov = cam.fieldOfView;
    }
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = target.position;
        targetpos = transform.position;
    }
    private void FixedUpdate()
    {
        if (updateType == UpdateType.FixedUpdate)
        {
            targetpos = Vector3.SmoothDamp(targetpos, target.position, ref vel, smoothness, Mathf.Infinity, Time.fixedDeltaTime);
            transform.position = targetpos + Offset;
            if (Vector3.Distance(Offset, targetOffset) > 0.01)
            {
                Offset = Vector3.SmoothDamp(Offset, targetOffset, ref vel1, snapSpeed, Mathf.Infinity, Time.fixedDeltaTime);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (updateType == UpdateType.Update)
        {
            targetpos = Vector3.SmoothDamp(targetpos, target.position, ref vel, smoothness);
            transform.position = targetpos + Offset;
            if (Vector3.Distance(Offset, targetOffset) > 0.01)
            {
                Offset = Vector3.SmoothDamp(Offset, targetOffset, ref vel1, snapSpeed);
            }
        }
    }
    public void Zoom(float z)
    {
        StartCoroutine(IZoom(z));
    }
    IEnumerator IZoom(float z)
    {
        float velo = 0;
        while (Mathf.Abs(zoom - z) > 0.1)
        {
            zoom = Mathf.SmoothDamp(zoom, z, ref velo, 0.08f);
            cam.fieldOfView = fov * zoom;
            yield return null;
        }
        zoom = z;
        cam.fieldOfView = fov * z;
    }
    public void EnterCharSelect()
    {
        targetOffset = charSelectOffset;
    }
    public void ExitCharSelect()
    {
        targetOffset = Vector3.zero;
    }
    public void EnterWorldSelect()
    {
        targetOffset = worldSelectOffset;
    }
}
