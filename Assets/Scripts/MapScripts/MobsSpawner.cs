using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobsSpawner : MonoBehaviour
{
    private const string TERRITORYOFMAPTAG = "TerritoryOfMap";
    private const string OBSTACLETAG = "Obstacle";

    [SerializeField] private float _radiusOfSpawn = 5f;
    [SerializeField] private int _amountOfMobs = 8;
    [SerializeField] private float _delayBetweenRespawn = 30f;
    [SerializeField] private GameObject _mobPrefab;
    [SerializeField] private List<GameObject> _mobsPool;
    private bool _inPlayerVision = false;

    private void Start()
    {
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
        for(int i = _mobsPool.Count - 1; i >= 0; i--)
            if (_mobsPool[i] == null)
                _mobsPool.Remove(_mobsPool[i]);
        while (_mobsPool.Count <= _amountOfMobs && !_inPlayerVision)
            SpawnNewMob();
    }

    private void SpawnNewMob()
    {
        GameObject spawnedMob = Instantiate(_mobPrefab, SpawnPos(), Quaternion.identity);
        _mobsPool.Add(spawnedMob);
        spawnedMob.transform.parent = gameObject.transform;
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
            if (collider.CompareTag(TERRITORYOFMAPTAG) && !collider.CompareTag(OBSTACLETAG))
                return pos;

        return SpawnPos();
    }
}