using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFrameRate : MonoBehaviour
{
    public int framerate;
    void Awake()
    {
        Application.targetFrameRate = framerate;
    }
}
