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
    private Vector3 basePosition = new Vector3(0f, 0f, 0f);
    private TypeGameObject _typeGameObject;
    private Camera _mainCamera;

    private void Start()
    {
        _typeGameObject = _mobPrefab.GetComponent<IMovebleObject>().Data.TypeGameObject;
        _mainCamera = Camera.main;
       SpawnLoop();
    }

    private void Update()
    {
        Vector3 viewportPoint = _mainCamera.WorldToViewportPoint(transform.position);
        _inPlayerVision = viewportPoint.z > 0 && viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;
    }

    private void SpawnLoop()
    {
        for (int i = 0; i < _amountOfMobs; i++)
        {
            GameObject instance = Instantiate(_mobPrefab);
            instance.transform.SetParent(transform);
            instance.transform.localPosition = basePosition;
            instance.transform.localPosition = RandomPosition();
            instance.SetActive(true);
            ItemActivator.ActivatorItemsAdd(instance);
        }
    }

    private Vector3 RandomPosition()
    {
        float posX = Random.Range(basePosition.x + _radiusOfSpawn, basePosition.x - _radiusOfSpawn);
        float posZ = Random.Range(basePosition.z + _radiusOfSpawn, basePosition.z - _radiusOfSpawn);
        Vector3 pos = new(posX, basePosition.y, posZ);
        return pos;
    }
}