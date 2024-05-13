using System.Collections;
using CapybaraRancher.EventBus;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    private const float REDUCE_VOLUME_RATE = 5f;
    private const float REDUCE_INTERVAL = 0.1f;

    [SerializeField] private AudioSource _walkAudio;
    [SerializeField] private AudioSource _runAudio;
    [SerializeField] private AudioSource _jumpAudio;
    [SerializeField] private AudioSource _gunRemoveAudio;
    [SerializeField] private AudioSource _gunAddAudio;
    [SerializeField] private AudioSource _gunAttractionAudio;

    private float _initialGunAttractionVolume;
    private Coroutine _volumeChangeCoroutine;

    private void OnEnable()
    {
        EventBus.PlayerMove += FootStepPlay;
        EventBus.PlayerJump += JumpPlay;
        EventBus.PlayerGunRemove += GunRemovePlay;
        EventBus.PlayerGunAdd += GunAddPlay;
        EventBus.PlayerGunAttraction += HandleGunAttraction;
    }
    private void OnDisable()
    {
        EventBus.PlayerMove -= FootStepPlay;
        EventBus.PlayerJump -= JumpPlay;
        EventBus.PlayerGunRemove -= GunRemovePlay;
        EventBus.PlayerGunAdd -= GunAddPlay;
        EventBus.PlayerGunAttraction -= HandleGunAttraction;
    }
    private void Start()
    {
        _initialGunAttractionVolume = _gunAttractionAudio.volume;
    }
    private void FootStepPlay(bool isMoving, bool isRunning)
    {
        if (isMoving)
        {
            if (!isRunning && !_walkAudio.isPlaying)
            {
                _walkAudio.Play();
                _runAudio.Stop();
            }
            else if (isRunning && !_runAudio.isPlaying)
            {
                _runAudio.Play();
                _walkAudio.Stop();
            }
        }
        else
        {
            _walkAudio.Stop();
            _runAudio.Stop();
        }
    }
    private void JumpPlay() => _jumpAudio.Play();
    private void GunRemovePlay() => _gunRemoveAudio.Play();
    private void GunAddPlay() => _gunAddAudio.Play();

    private void HandleGunAttraction(bool isAttracting)
    {
        if (isAttracting && !_gunAttractionAudio.isPlaying)
        {
            PlayGunAttraction();
        }
        else if (!isAttracting && _gunAttractionAudio.isPlaying)
        {
            StopGunAttraction();
        }
    }

    private void PlayGunAttraction()
    {
        if (_volumeChangeCoroutine != null)
        {
            StopCoroutine(_volumeChangeCoroutine);
        }
        _gunAttractionAudio.volume = 0f;
        _gunAttractionAudio.Play();
        _volumeChangeCoroutine = StartCoroutine(IncreaseGunAttractionVolume());
    }

    private void StopGunAttraction()
    {
        if (_volumeChangeCoroutine != null)
        {
            StopCoroutine(_volumeChangeCoroutine);
        }
        _volumeChangeCoroutine = StartCoroutine(ReduceGunAttractionVolume());
    }

    private IEnumerator ReduceGunAttractionVolume()
    {
        while (_gunAttractionAudio.volume > 0)
        {
            _gunAttractionAudio.volume -= _initialGunAttractionVolume / REDUCE_VOLUME_RATE;
            yield return new WaitForSeconds(REDUCE_INTERVAL);
        }
        _gunAttractionAudio.Stop();
    }

    private IEnumerator IncreaseGunAttractionVolume()
    {
        while (_gunAttractionAudio.volume < _initialGunAttractionVolume)
        {
            _gunAttractionAudio.volume += _initialGunAttractionVolume / REDUCE_VOLUME_RATE;
            yield return new WaitForSeconds(REDUCE_INTERVAL);
        }
    }
}
