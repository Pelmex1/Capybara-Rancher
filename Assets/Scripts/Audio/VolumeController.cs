using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    private string volumeParameter = "MasterVolume";
    
    private void Start()
    {
        var volumeValue = Mathf.Log10(PlayerPrefs.GetFloat("SliderVolume")) * 20f;
        mixer.SetFloat(volumeParameter, volumeValue);
    }
    private void Update()
    {
        var volumeValue = (PlayerPrefs.GetFloat("SliderVolume") * 100f) - 80f;
        mixer.SetFloat(volumeParameter, volumeValue);
    }
}
