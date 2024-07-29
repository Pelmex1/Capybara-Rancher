using CapybaraRancher.Enums;
using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;
using System.Collections;
using UnityEngine;

public class MobsSpawner : MonoBehaviour, IObjectSpawner
{
    [SerializeField] private float _radiusOfSpawn = 5f;
    [SerializeField] private int _amountOfMobs = 8;
    [SerializeField] private float _delayBetweenRespawn = 30f;
    [SerializeField] private GameObject _mobPrefab;

    private int _startMobsPool = 50;
    private bool _inPlayerVision = false;
    private TypeGameObject _typeGameObject;
    private int _activatedMobsCount = 0;
    private Camera _mainCamera;

    private void Start()
    {
        _typeGameObject = _mobPrefab.GetComponent<IMovebleObject>().Data.TypeGameObject;
        _mainCamera = Camera.main;
        InstantiateObjects(_startMobsPool);
        StartCoroutine(SpawnLoop());
    }

    private void InstantiateObjects(int number)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject spawnedObject = Instantiate(_mobPrefab);
            spawnedObject.transform.parent = null;
            spawnedObject.GetComponent<IService>().Init();  
        }
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            Vector3 viewportPoint = _mainCamera.WorldToViewportPoint(transform.position);
            _inPlayerVision = viewportPoint.z > 0 && viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;
            while (_activatedMobsCount < _amountOfMobs && !_inPlayerVision)
            {
                GameObject activatedObject = EventBus.RemoveFromThePool(_typeGameObject);

                activatedObject.transform.position = RandomPosition();
                ItemActivator.ActivatorItemsAdd(activatedObject);
                activatedObject.SetActive(true);
                _activatedMobsCount++;
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

    public void ReturnToPool(GameObject returnObject)
    {
        _activatedMobsCount--;
    }
}