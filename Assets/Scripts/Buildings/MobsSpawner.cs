using System.Collections;
using CapybaraRancher.EventBus;
using CapybaraRancher.Enums;
using CapybaraRancher.Interfaces;
using UnityEngine;

public class MobsSpawner : MonoBehaviour, IObjectSpawner
{
    private const string TERRITORY_OF_MAP_TAG = "TerritoryOfMap";
    private const string OBSTACLE_TAG = "Obstacle";

    [SerializeField] private float _radiusOfSpawn = 5f;
    [SerializeField] private int _amountOfMobs = 8;
    [SerializeField] private float _delayBetweenRespawn = 30f;
    [SerializeField] private int _startMobsPool = 50;
    [SerializeField] private GameObject _mobPrefab;

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

    private void Update()
    {
        Vector3 viewportPoint = _mainCamera.WorldToViewportPoint(transform.position);
        _inPlayerVision = viewportPoint.z > 0 && viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;
    }

    private void InstantiateObjects(int number)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject spawnedObject = Instantiate(_mobPrefab);
            EventBus.AddInPool(spawnedObject, _typeGameObject);
            spawnedObject.transform.parent = gameObject.transform;
        }
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            while (_activatedMobsCount < _amountOfMobs && !_inPlayerVision)
            {
                GameObject activatedObject = EventBus.RemoveFromThePool(_typeGameObject);
                activatedObject.transform.position = SpawnPos();
                ItemActivator.ActivatorItemsAdd(activatedObject);
                activatedObject.SetActive(true);
                _activatedMobsCount++;
            }
            yield return new WaitForSeconds(_delayBetweenRespawn);
        }
    }

    private Vector3 SpawnPos()
    {
        Vector3 pos = RandomPosition();
        Collider[] colliders = Physics.OverlapSphere(pos, 0.1f, 1 << LayerMask.NameToLayer("Default"));

        foreach (Collider collider in colliders)
            if (collider.CompareTag(TERRITORY_OF_MAP_TAG) && !collider.CompareTag(OBSTACLE_TAG))
                return pos;

        return SpawnPos();
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