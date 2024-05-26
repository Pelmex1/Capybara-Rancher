using System.Collections;
using DevionGames;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    [SerializeField] private AudioClip PlaceClip;
    [SerializeField] private AudioSource MusuicSourse;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ChangeClip());
        }
    }

    private IEnumerator ChangeClip()
    {
        MusuicSourse.volume -= Time.time;
        yield return new WaitUntil(() => MusuicSourse.volume <= 0);
        MusuicSourse.clip = PlaceClip;
        MusuicSourse.volume += Time.time;
        yield return new WaitUntil(() => MusuicSourse.volume >= 1);
        MusuicSourse.Play();
        PlayerPrefs.SetString("Music", PlaceClip.name);
        PlayerPrefs.Save();
    }
}
