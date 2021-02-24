using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolume : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider slider;
    public float soundVolume;

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("sound");
    }

    private void Update()
    {
        
    }

    public void UpdateVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);

        PlayerPrefs.SetFloat("sound", volume);
    }
}
