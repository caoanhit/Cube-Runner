using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeButton : MonoBehaviour
{
    public Slider slider;
    public AudioMixer audio;
    public GameObject muteButton;
    public GameObject unmuteButton;
    void Start()
    {
        CheckValue(slider.value);
    }

    // Update is called once per frame
    public void CheckValue(float value)
    {
        if (value <= 0.001f)
        {
            muteButton.SetActive(false);
            unmuteButton.SetActive(true);
        }
        else
        {
            muteButton.SetActive(true);
            unmuteButton.SetActive(false);
        }
    }
    public void ChangeVolume(float volume)
    {
        audio?.SetFloat("MusicVol", Mathf.Log10(volume) * 20);
    }
}
