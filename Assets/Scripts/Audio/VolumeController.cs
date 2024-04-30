using CapybaraRancher.EventBus;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;

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
            _mixer.SetFloat("MasterVolume", GetArray[0]);
            _mixer.SetFloat("MusicVolume", GetArray[1]);
            _mixer.SetFloat("SFXVolume", GetArray[2]);
            _mixer.SetFloat("AmbienceVolume", GetArray[2]);
            _mixer.SetFloat("PlayerVolume", GetArray[2]);
            _mixer.SetFloat("CapybaraVolume", GetArray[2]);
        }
        else
        {
            float midleValue = -30f;
            _mixer.SetFloat("MasterVolume", midleValue);
            _mixer.SetFloat("MusicVolume", midleValue);
            _mixer.SetFloat("SFXVolume", midleValue);
            _mixer.SetFloat("AmbienceVolume", midleValue);
            _mixer.SetFloat("PlayerVolume", midleValue);
            _mixer.SetFloat("CapybaraVolume", midleValue);
        }
    }

    private void SaveVolume(float[] SaveArray)
    {
        for (int i = 0; i < SaveArray.Length; i++)
            PlayerPrefs.SetFloat($"Mixer{i}", SaveArray[i]);
        PlayerPrefs.Save();
    }
}
