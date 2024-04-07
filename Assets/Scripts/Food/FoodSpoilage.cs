using System.Collections;
using UnityEngine;

public class FoodSpoilage : MonoBehaviour
{
    [SerializeField] private float _timeBeforeSpoilage = 30f;

    private float _waitingTime;
    private Renderer _objectRenderer;
    void Start()
    {
        _waitingTime = GetComponent<FoodItem>().TimeGeneration;
        _objectRenderer = GetComponent<Renderer>();
        StartCoroutine(SpoilagingLoop());
    }
    IEnumerator SpoilagingLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(_waitingTime * 2 - _timeBeforeSpoilage);
            Color startColor = _objectRenderer.material.color;
            float timeLeft = _timeBeforeSpoilage;

            while (timeLeft >= 0)
            {
                float percentOfRemainingTime = timeLeft / _timeBeforeSpoilage;
                _objectRenderer.material.color = Color.Lerp(Color.gray, startColor, percentOfRemainingTime);

                timeLeft -= Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }

            Destroy(gameObject);
        }
    }
}
