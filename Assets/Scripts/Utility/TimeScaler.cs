using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1;
    }
    public void SetTimeScale(float timescale)
    {
        Time.timeScale = timescale;
    }
}
