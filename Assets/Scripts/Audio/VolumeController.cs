using UnityEngine;
using UnityEngine.Audio;
using CustomEventBus;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    private void OnEnable()
    {
        EventBus.GetMusicValue += GetVolume;
        EventBus.SaveMusicValue += SaveVolume;
    }
    private void OnDisable()
    {
        EventBus.GetMusicValue -= GetVolume;
        EventBus.SaveMusicValue -= SaveVolume;
    }

    private void GetVolume(float[] GetArray)
    {
        if (PlayerPrefs.HasKey($"Mixer{0}"))
        {
            for (int i = 0; i < 3; i++)            
                GetArray[i] = PlayerPrefs.GetFloat($"Mixer{i}");            
            mixer.SetFloat("MasterVolume", GetArray[0]);
            mixer.SetFloat("MusicVolume", GetArray[1]);
            mixer.SetFloat("SFXVolume", GetArray[2]);
            mixer.SetFloat("AmbienceVolume", GetArray[2]);
            mixer.SetFloat("PlayerVolume", GetArray[2]);
            mixer.SetFloat("CapybaraVolume", GetArray[2]);
        }
        else
        {
            float midleValue = -30f;
            mixer.SetFloat("MasterVolume", midleValue);
            mixer.SetFloat("MusicVolume", midleValue);
            mixer.SetFloat("SFXVolume", midleValue);
            mixer.SetFloat("AmbienceVolume", midleValue);
            mixer.SetFloat("PlayerVolume", midleValue);
            mixer.SetFloat("CapybaraVolume", midleValue);
        }
    }

    private void SaveVolume(float[] SaveArray)
    {
        for (int i = 0; i < SaveArray.Length; i++)
            PlayerPrefs.SetFloat($"Mixer{i}", SaveArray[i]);
        PlayerPrefs.Save();
    }
}
