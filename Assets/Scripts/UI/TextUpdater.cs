using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour
{
    public IntVariable number;
    Text text;
    int value;
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (value != number.Value)
        {
            value = number.Value;
            text.text = value.ToString();
        }
    }
}
