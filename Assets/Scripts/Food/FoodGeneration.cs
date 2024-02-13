using System.Collections;
using UnityEngine;

public class FoodGeneration : MonoBehaviour
{
    [SerializeField] private GameObject foodPrefab;
    private float timeGeneration;
    void Start()
    {
        timeGeneration = foodPrefab.GetComponent<MovebleObject>().data.timeGeneration;

        StartCoroutine(StartGeneration());
    }
    IEnumerator StartGeneration()
    {
        while (true)
        {
            float delay = timeGeneration; // Задержка карутины равняется длительности роста урожая

            GameObject harvest = Instantiate(foodPrefab, transform.position, Quaternion.identity);
            harvest.transform.parent = transform;
            harvest.GetComponent<Rigidbody>().isKinematic = true;
            harvest.tag = "Untagged";

            harvest.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            StartCoroutine(Grow(harvest, delay));

            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator Grow(GameObject harvest, float delay)
    {
        Vector3 startSize = harvest.transform.localScale;
        Vector3 endSize = foodPrefab.transform.localScale;

        float currentTime = 0f;

        while (currentTime < delay)
        {
            harvest.transform.localScale = Vector3.Lerp(startSize, endSize, currentTime / delay);

            currentTime += Time.deltaTime;

            yield return null;
        }

        harvest.transform.localScale = endSize;
        harvest.GetComponent<Rigidbody>().isKinematic = false;
        harvest.tag = "movebleObject";
    }
}