using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine;

public class VolumeValueChange : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
    }
    public void SetLevel()
    {
        float slidervalue = slider.value;
        mixer.SetFloat("MusicVol", Mathf.Log10(slidervalue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", slidervalue);
    }
}