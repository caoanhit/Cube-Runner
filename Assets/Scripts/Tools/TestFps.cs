using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestFps : MonoBehaviour {
    Text fps;
    float time = 0;
    // Use this for initialization
    void Start () {
        fps = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        time+=Time.unscaledDeltaTime;
        if (time > 0.5f)
        {
            time = 0;
            fps.text = Mathf.Round(1.0f / Time.unscaledDeltaTime).ToString();
        }

    }
}
