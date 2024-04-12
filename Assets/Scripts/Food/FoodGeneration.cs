using DevionGames.UIWidgets;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class FoodGeneration : MonoBehaviour, IObjectSpawner
{
    private const string UNDERRIPE_TAG = "Untagged";
    private const string RIPE_TAG = "movebleObject";
    private const float START_GROWING_SCALE = 0.1f;

    [SerializeField] private GameObject _foodPrefab;
    [SerializeField] private int _startFoodPool = 50;

    private float _generationInterval;
    [SerializeField] private List<GameObject> _activeFoodsPool = new List<GameObject>();
    [SerializeField] private List<GameObject> _deactiveFoodsPool = new List<GameObject>();

    private void Start()
    {
        _generationInterval = _foodPrefab.GetComponent<IFoodItem>().TimeGeneration;
        InstantiateObjects(_startFoodPool);
        StartCoroutine(GenerationLoop());
    }

    private IEnumerator GenerationLoop()
    {
        while (true)
        {
            GameObject harvest = ActivateObject();
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
    private void InstantiateObjects(int number)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject spawnedObject = Instantiate(_foodPrefab, transform.position, Quaternion.identity);
            spawnedObject.SetActive(false);
            _deactiveFoodsPool.Add(spawnedObject);
            spawnedObject.transform.parent = gameObject.transform;
        }
    }

    private GameObject ActivateObject()
    {
        GameObject activatedObject = _deactiveFoodsPool[0];
        activatedObject.SetActive(true);
        _activeFoodsPool.Add(activatedObject);
        _deactiveFoodsPool.Remove(activatedObject);

        return activatedObject;
    }

    public void ReturnToPool(GameObject returnObject)
    {
        _activeFoodsPool.Remove(returnObject);
        _deactiveFoodsPool.Add(returnObject);
        returnObject.SetActive(false);
    }
}