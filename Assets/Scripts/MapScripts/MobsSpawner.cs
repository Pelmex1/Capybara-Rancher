using System.Collections;
using System.Collections.Generic;
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

    private List<GameObject> _activeMobsPool;
    private List<GameObject> _deactiveMobsPool;
    private bool _inPlayerVision = false;

    private void Start()
    {
        _activeMobsPool = new List<GameObject>();
        _deactiveMobsPool = new List<GameObject>();
        InstantiateObjects(_startMobsPool);
        StartCoroutine(SpawnLoop());
    }

    private void Update()
    {
        Camera mainCamera = Camera.main;
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(transform.position);
        _inPlayerVision = viewportPoint.z > 0 && viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            CheckValueOfMobs();
            yield return new WaitForSeconds(_delayBetweenRespawn);
        }
    }

    private void CheckValueOfMobs()
    {
        _activeMobsPool.RemoveAll(item => item == null);

        while (_activeMobsPool.Count < _amountOfMobs && !_inPlayerVision)
            ActivateObject();
    }

    private void InstantiateObjects(int number)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject spawnedObject = Instantiate(_mobPrefab);
            _deactiveMobsPool.Add(spawnedObject);
            spawnedObject.transform.parent = gameObject.transform;
        }
    }

    private void ActivateObject()
    {
        GameObject activatedObject = _deactiveMobsPool[0];
        activatedObject.transform.position = SpawnPos();
        _activeMobsPool.Add(activatedObject);
        _deactiveMobsPool.Remove(activatedObject);
        activatedObject.SetActive(true);
    }

    public void ReturnToPool(GameObject returnObject)
    {
        _deactiveMobsPool.Add(returnObject);
        _activeMobsPool.Remove(returnObject);
        ItemActivator.ActivatorItemsRemove(gameObject);
        returnObject.SetActive(false);
    }

    private Vector3 RandomPosition()
    {
        float posX = Random.Range(transform.position.x + _radiusOfSpawn, transform.position.x - _radiusOfSpawn);
        float posZ = Random.Range(transform.position.z + _radiusOfSpawn, transform.position.z - _radiusOfSpawn);
        Vector3 pos = new(posX, transform.position.y, posZ);
        return pos;
    }

    public Vector3 SpawnPos()
    {
        Vector3 pos = RandomPosition();
        Collider[] colliders = Physics.OverlapSphere(pos, 0.1f, 1 << LayerMask.NameToLayer("Default"));

        foreach (Collider collider in colliders)
            if (collider.CompareTag(TERRITORY_OF_MAP_TAG) && !collider.CompareTag(OBSTACLE_TAG))
                return pos;

        return SpawnPos();
    }
}