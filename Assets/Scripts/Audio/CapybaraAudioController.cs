using System.Collections;
using UnityEngine;

public class CapybaraAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource angryAudio;
    [SerializeField] private AudioSource happyAudio;
    [SerializeField] private AudioSource eatingAudio;
    private AudioSource noiseAudio;
    private void Start()
    {
        StartCoroutine(CapybaraNoiseLoop());
        noiseAudio = happyAudio;
    }

    private IEnumerator CapybaraNoiseLoop()
    {
        yield return new WaitForSecondsRealtime(Random.Range(5f, 10f));
        noiseAudio.Play();
    }

    public void SetHappyStatus()
    {
        noiseAudio = happyAudio;
    }

    public void SetAngryStatus()
    {
        noiseAudio = angryAudio;
    }

    public void Eating()
    {
        eatingAudio.Play();
    }
}
