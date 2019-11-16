using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CountDown : MonoBehaviour
{
    public int countdownTime;
    public Text counter;
    public Image image;
    public UnityEvent onTimesUp;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        time = countdownTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            counter.text = Mathf.Ceil(time).ToString();
            image.fillAmount = time / countdownTime;
            if (time <= 0)
            {
                time = 0;
                counter.text = "0";
                image.fillAmount = 0;
                onTimesUp.Invoke();
            }
        }
    }
    private void ResetTimer()
    {
        time = countdownTime;
    }
}
