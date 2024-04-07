using System.Collections;
using UnityEngine;

public class CapybaraAudioController : MonoBehaviour, ICapybaraAudioController
{
    private const float MIN_INTERVAL_TO_NOISE_SOUND = 5f;
    private const float MAX_INTERVAL_TO_NOISE_SOUND = 10f;

    [SerializeField] private AudioSource _angryAudio;
    [SerializeField] private AudioSource _happyAudio;
    [SerializeField] private AudioSource _eatingAudio;

    private AudioSource _noiseAudio;

    private void Start()
    {
        StartCoroutine(CapybaraNoiseLoop());
        _noiseAudio = _happyAudio;
    }

    private IEnumerator CapybaraNoiseLoop()
    {
        yield return new WaitForSecondsRealtime(Random.Range(MIN_INTERVAL_TO_NOISE_SOUND, MAX_INTERVAL_TO_NOISE_SOUND));
        _noiseAudio.Play();
    }

    public void SetHappyStatus() => _noiseAudio = _happyAudio;

    public void SetAngryStatus() => _noiseAudio = _angryAudio;

    public void Eating() => _eatingAudio.Play();
}
