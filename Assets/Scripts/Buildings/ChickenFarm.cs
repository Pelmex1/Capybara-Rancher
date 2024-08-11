using CapybaraRancher.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenFarm : MonoBehaviour
{
    private const string CHICKEN_NAME = "Chicken";
    private const float SPAWN_MISTAKE = 1f;

    [SerializeField] private float _delayBetweenSpawn = 120f;
    [SerializeField] private int _chickensLimit = 10;
    [SerializeField] private GameObject[] _chickenPrefabs;

    private bool _inPlayerVision = false;
    private Camera _mainCamera;
    private readonly List<IMovebleObject> _moveableObjects = new();

    private void Start()
    {
        _mainCamera = Camera.main;
        StartCoroutine(SpawnLoop());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IMovebleObject moveableObject))
            _moveableObjects.Add(moveableObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IMovebleObject moveableObject))
            _moveableObjects.Remove(moveableObject);
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            Vector3 viewportPoint = _mainCamera.WorldToViewportPoint(transform.position);
            _inPlayerVision = viewportPoint.z > 0 && viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;

            int[] chickens = new int[_chickenPrefabs.Length];
            if (!_inPlayerVision)
            {
                for (int i = 0; i < _moveableObjects.Count; i++)

                    if (_moveableObjects[i].Data.Name.Contains(CHICKEN_NAME))

                        for (int j = 0; j < _chickenPrefabs.Length; j++)

                            if (_moveableObjects[i].Data.Prefab = _chickenPrefabs[j])
                                chickens[j]++;

                for (int i = 0; i < chickens.Length; i++)

                    for (int j = 0; j < chickens[i] / 2; j++)
                    {
                        int chickensCount = 0;
                        for (int l = 0; l < chickens.Length; l++)
                            chickensCount += chickens[l];
                        if (chickensCount <= _chickensLimit)
                            SpawnNewChicken(i);
                    }
            }

            yield return new WaitForSeconds(_delayBetweenSpawn);
        }
    }

    private void SpawnNewChicken(int typeNumber)
    {
        GameObject spawnedObject = Instantiate(_chickenPrefabs[typeNumber]);
        spawnedObject.transform.SetParent(transform);
        spawnedObject.transform.position = new Vector3(transform.position.x, transform.position.y + SPAWN_MISTAKE, transform.position.z);
        ItemActivator.ActivatorItemsAdd(spawnedObject);
        spawnedObject.SetActive(true);
    }
}
