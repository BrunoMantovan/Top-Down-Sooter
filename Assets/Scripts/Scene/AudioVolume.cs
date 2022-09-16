using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolume : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixerGroup sfx;


 public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
                
    }
    public void SfxVolume(float volume)
    {
        sfx.audioMixer.SetFloat("SFX", volume);
    }
}
