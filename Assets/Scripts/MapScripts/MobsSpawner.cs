using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobsSpawner : MonoBehaviour
{
    [SerializeField] private float radiusOfSpawn = 5f;
    [SerializeField] private int amountOfMobs = 8;
    [SerializeField] private float delayBetweenRespawn = 30f;
    [SerializeField] private GameObject mobPrefab;
    [SerializeField] private List<GameObject> mobsPool;
    private bool inPlayerVision = false;

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }
    private void Update()
    {
        Camera mainCamera = Camera.main;
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(transform.position);
        inPlayerVision = viewportPoint.z > 0 && viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;

    }
    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            CheckValueOfMobs();
            yield return new WaitForSeconds(delayBetweenRespawn);
        }
    }
    private void CheckValueOfMobs()
    {
        for(int i = mobsPool.Count - 1; i >= 0; i--)
            if (mobsPool[i] == null)
                mobsPool.Remove(mobsPool[i]);
        while (mobsPool.Count <= amountOfMobs && !inPlayerVision)
            SpawnNewMob();
    }
    private void SpawnNewMob()
    {
        GameObject spawnedMob = Instantiate(mobPrefab, SpawnPos(), Quaternion.identity);
        mobsPool.Add(spawnedMob);
        spawnedMob.transform.parent = gameObject.transform;
    }
    private Vector3 RandomPosition()
    {
        float posX = Random.Range(transform.position.x + radiusOfSpawn, transform.position.x - radiusOfSpawn);
        float posZ = Random.Range(transform.position.z + radiusOfSpawn, transform.position.z - radiusOfSpawn);
        Vector3 pos = new(posX, transform.position.y, posZ);
        return pos;
    }
    public Vector3 SpawnPos()
    {
        Vector3 pos = RandomPosition();
        Collider[] colliders = Physics.OverlapSphere(pos, 0.1f, 1 << LayerMask.NameToLayer("Default"));

        foreach (Collider collider in colliders)
            if (collider.CompareTag("TerritoryOfMap") && !collider.CompareTag("Obstacle"))
                return pos;

        return SpawnPos();
    }
}