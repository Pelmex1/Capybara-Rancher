using System.Collections;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    [SerializeField] private AudioClip PlaceClip;
    [SerializeField] private AudioSource MusuicSourse;
    [SerializeField] private float fadeDuration = 2.0f;
    private void Start()
    {
        if(PlaceClip.name == PlayerPrefs.GetString("ChangeMusic"))
        {
            MusuicSourse.clip = PlaceClip;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ChangeClip());
        }
    }

    private IEnumerator ChangeClip()
    {
        if (PlaceClip.name != PlayerPrefs.GetString("ChangeMusic"))
        {
            float startVolume = MusuicSourse.volume;
            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                MusuicSourse.volume = Mathf.Lerp(startVolume, 0, elapsedTime / fadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            MusuicSourse.volume = 0;
            MusuicSourse.clip = PlaceClip;
            elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                MusuicSourse.volume = Mathf.Lerp(0, startVolume, elapsedTime / fadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            MusuicSourse.volume = startVolume;
            MusuicSourse.Play();
            PlayerPrefs.SetString("ChangeMusic", PlaceClip.name);
        }
    }
}
