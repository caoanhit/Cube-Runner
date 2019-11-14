using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmoothTextUpdater : MonoBehaviour
{
    public IntVariable number;
    [Range(0, 1)]
    public float updateSpeed = 0.5f;
    Text text;
    int value;
    float smoothValue;
    float vel;
    void Start()
    {
        text = GetComponent<Text>();
        value = number;
        smoothValue = number;
        text.text = value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (number != value)
        {
            smoothValue = Mathf.SmoothDamp(smoothValue, number, ref vel, updateSpeed);
            value = (int)smoothValue;
            text.text = value.ToString();
            if (number == value)
            {
                smoothValue = value;
            }
        }
    }
}
