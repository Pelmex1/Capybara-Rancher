using System.Collections;
using UnityEngine;

public class FoodGeneration : MonoBehaviour
{
    private const string underripeTag = "Untagged";
    private const string ripeTag = "movebleObject";

    [SerializeField] private GameObject foodPrefab;

    private float generationInterval;

    private void Start()
    {
        generationInterval = foodPrefab.GetComponent<FoodItem>().timeGeneration;

        StartCoroutine(GenerationLoop());
    }

    IEnumerator GenerationLoop()
    {
        while (true)
        {
            GameObject harvest = Instantiate(foodPrefab, transform.position, Quaternion.identity);
            harvest.transform.parent = transform;
            Rigidbody harvestRB = harvest.GetComponent<Rigidbody>();
            harvestRB.isKinematic = true;
            harvest.tag = underripeTag;
            harvest.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            Vector3 startSize = harvest.transform.localScale;
            Vector3 endSize = foodPrefab.transform.localScale;

            float currentTime = 0f;

            while (currentTime < generationInterval)
            {
                harvest.transform.localScale = Vector3.Lerp(startSize, endSize, currentTime / generationInterval);

                currentTime += Time.deltaTime;

                yield return null;
            }

            harvest.transform.localScale = endSize;
            harvestRB.isKinematic = false;
            harvest.tag = ripeTag;
        }
    }
}