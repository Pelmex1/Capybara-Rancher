using System.Collections;
using UnityEngine;

public class FoodGeneration : MonoBehaviour
{
    private const string UNDERRIPE_TAG = "Untagged";
    private const string RIPE_TAG = "movebleObject";
    private const float START_GROWING_SCALE = 0.1f;

    [SerializeField] private GameObject _foodPrefab;

    private float _generationInterval;

    private void Start()
    {
        _generationInterval = _foodPrefab.GetComponent<IFoodItem>().TimeGeneration;

        StartCoroutine(GenerationLoop());
    }

    IEnumerator GenerationLoop()
    {
        while (true)
        {
            GameObject harvest = Instantiate(_foodPrefab, transform.position, Quaternion.identity);
            harvest.transform.parent = transform;
            Rigidbody harvestRB = harvest.GetComponent<Rigidbody>();
            harvestRB.isKinematic = true;
            harvest.tag = UNDERRIPE_TAG;
            harvest.transform.localScale = new Vector3(START_GROWING_SCALE, START_GROWING_SCALE, START_GROWING_SCALE);

            Vector3 startSize = harvest.transform.localScale;
            Vector3 endSize = _foodPrefab.transform.localScale;

            float currentTime = 0f;

            while (currentTime < _generationInterval)
            {
                harvest.transform.localScale = Vector3.Lerp(startSize, endSize, currentTime / _generationInterval);

                currentTime += Time.deltaTime;

                yield return null;
            }

            harvest.transform.localScale = endSize;
            harvestRB.isKinematic = false;
            harvest.tag = RIPE_TAG;
        }
    }
}