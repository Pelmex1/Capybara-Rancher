using System.Collections;
using System.Collections.Generic;
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
        eventBus.ChangeVolume += SetVolume;
    }
    private void OnDisable()
    {
        eventBus.ChangeVolume -= SetVolume;
    }
    private void Start()
    {
        SetVolume();
    }
    private void SetVolume()
    {
        float volumeValue = (PlayerPrefs.GetFloat("SliderVolume") * 100f) - 80f;
        mixer.SetFloat(volumeParameter, volumeValue);
    }
}
