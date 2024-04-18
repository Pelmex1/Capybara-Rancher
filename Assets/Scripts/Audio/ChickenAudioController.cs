using System.Collections;
using UnityEngine;

public class ChickenAudioController : MonoBehaviour
{
    private const float MIN_SOUND_INTERVAL = 5f;
    private const float MAX_SOUND_INTERVAL = 10f;

    [SerializeField] private AudioSource _noiseAudio;

    private void Start()
    {
        StartCoroutine(NoiseLoop());
    }

    private IEnumerator NoiseLoop()
    {
        yield return new WaitForSecondsRealtime(Random.Range(MIN_SOUND_INTERVAL, MAX_SOUND_INTERVAL));
        _noiseAudio.Play();
    }
}
