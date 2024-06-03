using CapybaraRancher.Enums;
using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;
using UnityEngine;

public class PoolSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _mobPrefab;
    [SerializeField] private int _startMobsPool = 50;

    private TypeGameObject _type;
    private void Start() {
        _type = _mobPrefab.GetComponent<IMovebleObject>().Data.TypeGameObject;
        for (int i = 0; i < _startMobsPool; i++)
        {
            GameObject instance = Instantiate(_mobPrefab);
            instance.SetActive(false);
            EventBus.AddInPool(instance,_type);
        }
    }
}
