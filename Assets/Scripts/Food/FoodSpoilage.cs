using System.Collections;
using UnityEngine;

public class FoodSpoilage : MonoBehaviour
{
    [SerializeField] private float timeBeforeSpoilage = 30f;

    private float waitingTime;
    private Renderer objectRenderer;
    void Start()
    {
        waitingTime = GetComponent<FoodItem>().timeGeneration;
        objectRenderer = GetComponent<Renderer>();
        StartCoroutine(SpoilagingLoop());
    }
    IEnumerator SpoilagingLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitingTime * 2 - timeBeforeSpoilage);
            Color startColor = objectRenderer.material.color;
            float timeLeft = timeBeforeSpoilage;

            while (timeLeft >= 0)
            {
                float percentOfRemainingTime = timeLeft / timeBeforeSpoilage;
                objectRenderer.material.color = Color.Lerp(Color.gray, startColor, percentOfRemainingTime);

                timeLeft -= Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }

            Destroy(gameObject);
        }
    }
}
