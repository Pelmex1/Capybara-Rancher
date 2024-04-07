using System.Collections.Generic;
using CustomEventBus;
using UnityEngine;

public class Receptacle : MonoBehaviour
{
    [SerializeField] private Collider _gettingCollider;
    [SerializeField] private GameObject _emptyFarm;
    [SerializeField] private List<GameObject> _farmPrefabs;

    public Transform SpawnTransform;
    public GameObject NewPlace;

    private void OnEnable()
    {
        EventBus.WasChangeFarm += ChangeFarm;
    }

    private void OnDisable()
    {
        EventBus.WasChangeFarm -= ChangeFarm;
    }

    public void GetData(Transform ParentPosition, GameObject NewObject)
    {
        SpawnTransform = ParentPosition;
        NewObject = NewPlace;
    }

    public void ChangeFarm(int spawnIndex)
    {
        NewPlace = Instantiate(_farmPrefabs[spawnIndex], SpawnTransform.position, SpawnTransform.rotation);
        Destroy(_emptyFarm);

        GetComponent<Receptacle>().enabled = false;
    }
}


