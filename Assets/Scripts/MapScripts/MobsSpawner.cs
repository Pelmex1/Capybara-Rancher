using System.Collections;
using CapybaraRancher.EventBus;
using CapybaraRancher.Enums;
using CapybaraRancher.Interfaces;
using UnityEngine;
using System.Collections.Generic;

public class MobsSpawner : MonoBehaviour
{
    [SerializeField] private float _radiusOfSpawn = 5f;
    [SerializeField] private int _amountOfMobs = 8;
    [SerializeField] private float _delayBetweenRespawn = 30f;
    [SerializeField] private GameObject _mobPrefab;

    private bool _inPlayerVision = false;
    private TypeGameObject _typeGameObject;
    private Camera _mainCamera;
    private Queue<GameObject> _spawndeGameObjects;

    private void Start()
    {
        _typeGameObject = _mobPrefab.GetComponent<IMovebleObject>().Data.TypeGameObject;
        _mainCamera = Camera.main;
        StartCoroutine(SpawnLoop());
    }

    private void Update()
    {
        Vector3 viewportPoint = _mainCamera.WorldToViewportPoint(transform.position);
        _inPlayerVision = viewportPoint.z > 0 && viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;
    }

    private IEnumerator SpawnLoop()
    {
        for (int i = 0; i < _amountOfMobs; i++)
        {
            GameObject instance = Instantiate(_mobPrefab);
            instance.SetActive(false);
            _spawndeGameObjects.Enqueue(instance);
        }
        while (true)
        {
            while (transform.childCount < _amountOfMobs && !_inPlayerVision)
            {
                GameObject activatedObject = _spawndeGameObjects.Dequeue();
                activatedObject.transform.position = RandomPosition();
                activatedObject.transform.SetParent(transform);
                ItemActivator.ActivatorItemsAdd(activatedObject);
                activatedObject.SetActive(true);
            }
            yield return new WaitForSeconds(_delayBetweenRespawn);
        }
    }

    private Vector3 RandomPosition()
    {
        float posX = Random.Range(transform.position.x + _radiusOfSpawn, transform.position.x - _radiusOfSpawn);
        float posZ = Random.Range(transform.position.z + _radiusOfSpawn, transform.position.z - _radiusOfSpawn);
        Vector3 pos = new(posX, transform.position.y, posZ);
        return pos;
    }
}