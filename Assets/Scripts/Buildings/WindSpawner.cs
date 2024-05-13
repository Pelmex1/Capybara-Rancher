using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpawner : MonoBehaviour
{
    private const float MIN_DISTANCE_FOR_X_AND_Z = -30f;
    private const float MAX_DISTANCE_FOR_X_AND_Z = 30f;
    private const float MIN_DISTANCE_FOR_Y = 5f;
    private const float MAX_DISTANCE_FOR_Y = 10f;
    private const float DESTROY_DELAY = 5f;

    [SerializeField] private Transform _player;
    [SerializeField] private float _intervalBetweenWindGenerete = 2f;
    [SerializeField] private GameObject[] _windsPrefabs;
    private List<GameObject> _activeWindsPool = new List<GameObject>();
    private List<GameObject> _deactiveWindsPool = new List<GameObject>();

    private void Start()
    {
        InstantiateObjects(25);
        StartCoroutine(StartWindSpawn());
    }

    private IEnumerator StartWindSpawn()
    {
        while (true)
        {
            Vector3 spawnPos = _player.position;
            spawnPos.x += Random.Range(MIN_DISTANCE_FOR_X_AND_Z, MAX_DISTANCE_FOR_X_AND_Z);
            spawnPos.y += Random.Range(MIN_DISTANCE_FOR_Y, MAX_DISTANCE_FOR_Y);
            spawnPos.z += Random.Range(MIN_DISTANCE_FOR_X_AND_Z, MAX_DISTANCE_FOR_X_AND_Z);

            StartCoroutine(ReturnToPoolObject(ActiveObject(spawnPos)));

            yield return new WaitForSecondsRealtime(_intervalBetweenWindGenerete);
        }
    }
    private void InstantiateObjects(int number)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject spawnedObject = Instantiate(_windsPrefabs[Random.Range(0, _windsPrefabs.Length - 1)]);
            _deactiveWindsPool.Add(spawnedObject);
            spawnedObject.transform.parent = transform;
            spawnedObject.SetActive(false);
        }
    }

    private GameObject ActiveObject(Vector3 spawnPos)
    {
        GameObject activatedObject = _deactiveWindsPool[0];
        activatedObject.SetActive(true);
        _activeWindsPool.Add(activatedObject);
        _deactiveWindsPool.Remove(activatedObject);
        activatedObject.transform.position = spawnPos;

        return activatedObject;
    }
    private IEnumerator ReturnToPoolObject(GameObject returnObject)
    {
        yield return new WaitForSecondsRealtime(DESTROY_DELAY);

        returnObject.SetActive(false);
        _deactiveWindsPool.Add(returnObject);
        _activeWindsPool.Remove(returnObject);
    }
}
