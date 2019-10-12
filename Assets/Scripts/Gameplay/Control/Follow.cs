using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public Vector3 speed;
    public Vector3 offset;
    public UpdateType updateType;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (updateType == UpdateType.Update)
        {
            Vector3 t = target.position;
            transform.position = new Vector3(t.x * speed.x, t.y * speed.y, t.z * speed.z) + offset;
        }
    }
    private void FixedUpdate()
    {
        if (updateType == UpdateType.FixedUpdate)
        {
            Vector3 t = target.position;
            transform.position = new Vector3(t.x * speed.x, t.y * speed.y, t.z * speed.z) + offset;
        }
    }
}
