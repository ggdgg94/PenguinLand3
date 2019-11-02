using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class VolumeValueChange : MonoBehaviour
{

    // Reference to Audio Source component
    public AudioMixer mixer;
    public void Setlevel(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }

}