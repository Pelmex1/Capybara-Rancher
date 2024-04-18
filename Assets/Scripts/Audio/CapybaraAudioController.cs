using System.Collections;
using UnityEngine;

public class CapybaraAudioController : MonoBehaviour, ICapybaraAudioController
{
    private const float MIN_SOUND_INTERVAL = 5f;
    private const float MAX_SOUND_INTERVAL = 10f;

    [SerializeField] private AudioSource _angryAudio;
    [SerializeField] private AudioSource _happyAudio;
    [SerializeField] private AudioSource _eatingAudio;

    private AudioSource _noiseAudio;

    private void Start()
    {
        StartCoroutine(NoiseLoop());
        _noiseAudio = _happyAudio;
    }

    private IEnumerator NoiseLoop()
    {
        yield return new WaitForSecondsRealtime(Random.Range(MIN_SOUND_INTERVAL, MAX_SOUND_INTERVAL));
        _noiseAudio.Play();
    }

    public void SetHappyStatus() => _noiseAudio = _happyAudio;

    public void SetAngryStatus() => _noiseAudio = _angryAudio;

    public void Eating() => _eatingAudio.Play();
}
