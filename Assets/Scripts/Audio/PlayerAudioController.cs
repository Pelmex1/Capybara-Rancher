using System;
using System.Collections;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private event Action sliderCheck;

    [SerializeField] private AudioSource walkAudio;
    [SerializeField] private AudioSource runAudio;
    [SerializeField] private AudioSource jumpAudio;
    [SerializeField] private AudioSource gunAttractionAudio;
    [SerializeField] private AudioSource gunRemoveAudio;
    [SerializeField] private AudioSource gunAddAudio;
    private float initialGunAttractionVolume;

    private void Start()
    {
        initialGunAttractionVolume = gunAttractionAudio.volume;
        //SetVolume();
    }
    /*private void SetVolume()
    {
        walkAudio.volume = PlayerPrefs.GetFloat("SliderVolume", 0.1f);
        runAudio.volume = PlayerPrefs.GetFloat("SliderVolume", 0.1f);
        jumpAudio.volume = PlayerPrefs.GetFloat("SliderVolume", 0.1f);
        gunAttractionAudio.volume = PlayerPrefs.GetFloat("SliderVolume", 0.1f);
        gunRemoveAudio.volume = PlayerPrefs.GetFloat("SliderVolume", 0.1f);
        gunAddAudio.volume = PlayerPrefs.GetFloat("SliderVolume", 0.1f);
    }*/
    public void FootStepPlay(bool isGrounded, bool isRunning)
    {
        bool moving = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && isGrounded;
        if (moving)
        {
            if (!isRunning && !walkAudio.isPlaying)
            {
                walkAudio.Play();
                runAudio.Stop();
            }
            else if (isRunning && !runAudio.isPlaying)
            {
                runAudio.Play();
                walkAudio.Stop();
            }
        }
        else
        {
            walkAudio.Stop();
            runAudio.Stop();
        }
    }
    public void JumpPlay()
    {
        jumpAudio.Play();
    }
    public void GunAttractionPlay()
    {
        if (!(Input.GetMouseButton(0) && Cursor.lockState == CursorLockMode.Locked))
        {
            StartCoroutine(ReduceGunAttractionVolume());
        }
        else if (!gunAttractionAudio.isPlaying)
        {
            StartCoroutine(IncreaseGunAttractionVolume());
        }
    }
    IEnumerator ReduceGunAttractionVolume()
    {
        while (gunAttractionAudio.volume > 0)
        {
            gunAttractionAudio.volume -= initialGunAttractionVolume / 5f;
            yield return new WaitForSeconds(0.1f);
        }
        gunAttractionAudio.Stop();
    }
    IEnumerator IncreaseGunAttractionVolume()
    {
        gunAttractionAudio.Play();
        while (gunAttractionAudio.volume < initialGunAttractionVolume)
        {
            gunAttractionAudio.volume += initialGunAttractionVolume / 5f;
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void GunRemovePlay()
    {
        gunRemoveAudio.Play();
    }
    public void GunAddPlay()
    {
        gunAddAudio.Play();
    }
}
