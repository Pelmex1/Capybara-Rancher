using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobsSpawner : MonoBehaviour, IMobsSpawner
{
    private const string TERRITORY_OF_MAP_TAG = "TerritoryOfMap";
    private const string OBSTACLE_TAG = "Obstacle";

    [SerializeField] private float _radiusOfSpawn = 5f;
    [SerializeField] private int _amountOfMobs = 8;
    [SerializeField] private float _delayBetweenRespawn = 30f;
    [SerializeField] private int _startMobsPool = 50;
    [SerializeField] private GameObject _mobPrefab;
    [SerializeField] private List<GameObject> _activeMobsPool;
    [SerializeField] private List<GameObject> _deactiveMobsPool;
    private bool _inPlayerVision = false;

    private void Start()
    {
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
        for(int i = _activeMobsPool.Count - 1; i >= 0; i--)
            if (_activeMobsPool[i] == null)
                _activeMobsPool.Remove(_activeMobsPool[i]);
        while (_activeMobsPool.Count < _amountOfMobs && !_inPlayerVision)
            ActivateNewMob();
    }

    private void InstantiateObjects(int number)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject spawnedMob = Instantiate(_mobPrefab);
            _deactiveMobsPool.Add(spawnedMob);
            spawnedMob.transform.parent = gameObject.transform;
        }
    }

    private void ActivateNewMob()
    {
        GameObject newMob = _deactiveMobsPool[0];
        newMob.SetActive(true);
        newMob.transform.position = SpawnPos();
        _activeMobsPool.Add(newMob);
        _deactiveMobsPool.Remove(newMob);
    }

    public void ReturnToPool(GameObject returnObject)
    {
        _activeMobsPool.Remove(returnObject);
        _deactiveMobsPool.Add(returnObject);
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