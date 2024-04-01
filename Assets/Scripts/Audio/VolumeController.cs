using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    private string volumeParameter = "MasterVolume";

    private EventBus eventBus;


    private void OnEnable()
    {
        eventBus = EventBus.eventBus;
        eventBus.GetMisicValue += GetVolume;
        eventBus.SaveMusicValue += SaveVolume;
    }
    private void OnDisable()
    {
        eventBus.GetMisicValue -= GetVolume;
        eventBus.SaveMusicValue -= SaveVolume;
    }

    private void GetVolume(float[] GetArray)
    {
        if (PlayerPrefs.HasKey($"Mixer{0}"))
        {
            for (int i = 0; i < 3; i++)
            {
                Debug.Log($"Work {PlayerPrefs.GetFloat($"Mixer{i}")}");
                GetArray[i] = PlayerPrefs.GetFloat($"Mixer{i}");
            }
            mixer.SetFloat("MasterVolume", GetArray[0]);
            mixer.SetFloat("MusicVolume", GetArray[1]);
            mixer.SetFloat("SFXVolume", GetArray[2]);
            mixer.SetFloat("AmbienceVolume", GetArray[2]);
            mixer.SetFloat("PlayerVolume", GetArray[2]);
            mixer.SetFloat("CapybaraVolume", GetArray[2]);
        }
    }

    private void SaveVolume(float[] SaveArray)
    {
        for (int i = 0; i < SaveArray.Length; i++)
            PlayerPrefs.SetFloat($"Mixer{i}", SaveArray[i]);
        PlayerPrefs.Save();
    }
}
