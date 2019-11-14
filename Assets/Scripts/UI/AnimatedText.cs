using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Text))]
public class AnimatedText : MonoBehaviour
{
    public IntVariable number;
    Text text;
    Animator anim;
    int value;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        text = GetComponent<Text>();
        value = number;
    }

    // Update is called once per frame
    private void Update()
    {
        if (number != value)
        {
            anim?.SetTrigger("Play");

            if (number - value > 0)
            {
                text.text = "+" + (number - value).ToString();
            }
            else
            {
                text.text = (number - value).ToString();
            }
            value = number;
        }
    }
}
