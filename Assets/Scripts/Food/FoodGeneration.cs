using CapybaraRancher.Enums;
using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;
using System.Collections;
using UnityEngine;

public class FoodGeneration : MonoBehaviour, IObjectSpawner
{
    private const string UNDERRIPE_TAG = "Untagged";
    private const string RIPE_TAG = "movebleObject";
    private const float START_GROWING_SCALE = 0.1f;

    [SerializeField] private GameObject _foodPrefab;
    [SerializeField] private int _startFoodPool = 20;

    private float _generationInterval;
    private TypeGameObject _typeGameObject;

    private void Start()
    {
        _typeGameObject = _foodPrefab.GetComponent<IMovebleObject>().Data.TypeGameObject;
        _generationInterval = _foodPrefab.GetComponent<IFoodItem>().TimeGeneration;

        InstantiateObjects(_startFoodPool);
        StartCoroutine(GenerationLoop());
    }

    private IEnumerator GenerationLoop()
    {
        while (true)
        {
            GameObject harvest = EventBus.RemoveFromThePool(_typeGameObject);
            Rigidbody harvestRB = harvest.GetComponent<Rigidbody>();
            harvestRB.isKinematic = true;
            harvest.tag = UNDERRIPE_TAG;
            harvest.transform.localScale = new Vector3(START_GROWING_SCALE, START_GROWING_SCALE, START_GROWING_SCALE);
            harvest.transform.position = transform.position;
            harvest.SetActive(true);

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
    private void InstantiateObjects(int number)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject spawnedObject = Instantiate(_foodPrefab);
            spawnedObject.SetActive(false);
        }
    }

    public void ReturnToPool(GameObject returnObject)
    {
        returnObject.SetActive(false);
    }
}