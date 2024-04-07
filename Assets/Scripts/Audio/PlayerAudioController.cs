using CustomEventBus;
using System.Collections;
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

    private void OnEnable()
    {
        EventBus.PlayerMove += FootStepPlay;
        EventBus.PlayerJump += JumpPlay;
        EventBus.PlayerGunRemove += GunRemovePlay;
        EventBus.PlayerGunAdd += GunAddPlay;
        EventBus.PlayerGunAttraction += GunAttractionPlay;
    }
    private void OnDisable()
    {
        EventBus.PlayerMove -= FootStepPlay;
        EventBus.PlayerJump -= JumpPlay;
        EventBus.PlayerGunRemove -= GunRemovePlay;
        EventBus.PlayerGunAdd -= GunAddPlay;
        EventBus.PlayerGunAttraction -= GunAttractionPlay;
    }
    private void Start()
    {
        _initialGunAttractionVolume = _gunAttractionAudio.volume;
    }
    public void FootStepPlay(bool isGrounded, bool isRunning)
    {
        static bool IsMoving() => Input.inputString switch {
            "W" => true,
            "A" => true,
            "S" => true,
            "D" => true,
            _ => false,
        };
        bool moving = IsMoving() && isGrounded;
        if (moving)
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
    
    private void GunAttractionPlay()
    {
        if (!(Input.GetMouseButton(0) && Cursor.lockState == CursorLockMode.Locked))
        {
            StartCoroutine(ReduceGunAttractionVolume());
        }
        else if (!_gunAttractionAudio.isPlaying)
        {
            StartCoroutine(IncreaseGunAttractionVolume());
        }
    }
    IEnumerator ReduceGunAttractionVolume()
    {
        while (_gunAttractionAudio.volume > 0)
        {
            _gunAttractionAudio.volume -= _initialGunAttractionVolume / REDUCE_VOLUME_RATE;
            yield return new WaitForSeconds(REDUCE_INTERVAL);
        }
        _gunAttractionAudio.Stop();
    }
    IEnumerator IncreaseGunAttractionVolume()
    {
        _gunAttractionAudio.Play();
        while (_gunAttractionAudio.volume < _initialGunAttractionVolume)
        {
            _gunAttractionAudio.volume += _initialGunAttractionVolume / REDUCE_VOLUME_RATE;
            yield return new WaitForSeconds(REDUCE_INTERVAL);
        }
    }
}
