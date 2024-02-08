using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpoilage : MonoBehaviour
{
    [SerializeField] private float timeBeforeSpoilage = 30f;

    private float waitingTime;
    private Renderer objectRenderer;
    void Start()
    {
        waitingTime = GetComponent<ResourseData>().timeGeneration;
        objectRenderer = GetComponent<Renderer>();
        StartCoroutine(TimerForSpoilage());
    }
    IEnumerator TimerForSpoilage()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitingTime * 2 - timeBeforeSpoilage);
            StartCoroutine(Spoilaging());
        }
    }
    IEnumerator Spoilaging()
    {
        Color startColor = objectRenderer.material.color;
        float timeLeft = timeBeforeSpoilage;
        while(timeLeft >= 0)
        {
            float i = timeLeft / timeBeforeSpoilage;
            objectRenderer.material.color = Color.Lerp(Color.gray, startColor, i);

            timeLeft -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Destroy(gameObject);
    }
}
