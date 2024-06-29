using UnityEngine;

public class MobsSpawner : MonoBehaviour
{
    [SerializeField] private float _radiusOfSpawn = 5f;
    [SerializeField] private int _amountOfMobs = 8;
    [SerializeField] private GameObject _mobPrefab;
    private Vector3 basePosition = new(0f, 0f, 0f);
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
       SpawnLoop();
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