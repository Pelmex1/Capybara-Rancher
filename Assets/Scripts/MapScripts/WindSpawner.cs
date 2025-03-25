using System.Collections;
using System.Collections.Generic;
using CapybaraRancher.Consts;
using UnityEngine;

public class WindSpawner : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _intervalBetweenWindGenerete = 2f;
    [SerializeField] private float _minDistanceForXAndZ = -30f;
    [SerializeField] private float _maxDistanceForXAndZ = 30f;
    [SerializeField] private GameObject[] _windsPrefabs;
    private List<GameObject> _activeWindsPool = new List<GameObject>();
    private List<GameObject> _deactiveWindsPool = new List<GameObject>();

    private void Start()
    {
        if(_player == null)
        {
            return;
        }
        InstantiateObjects(25);
        StartCoroutine(StartWindSpawn());
    }

    private IEnumerator StartWindSpawn()
    {
        while (true)
        {
            Vector3 spawnPos = _player.position;
            spawnPos.x += Random.Range(_minDistanceForXAndZ, _maxDistanceForXAndZ);
            spawnPos.y += Random.Range(Constants.MIN_DISTANCE_FOR_Y, Constants.MAX_DISTANCE_FOR_Y);
            spawnPos.z += Random.Range(_minDistanceForXAndZ, _maxDistanceForXAndZ);

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
        yield return new WaitForSecondsRealtime(Constants.DESTROY_DELAY);

        returnObject.SetActive(false);
        _deactiveWindsPool.Add(returnObject);
        _activeWindsPool.Remove(returnObject);
    }
}
