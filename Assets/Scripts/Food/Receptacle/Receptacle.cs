using System.Collections.Generic;
using CustomEventBus;
using UnityEngine;
public class Receptacle : MonoBehaviour
{
    [SerializeField] private Collider gettingCollider;
    [SerializeField] private GameObject emptyFarm;
    [SerializeField] private List<GameObject> farmPrefabs;
    

    public Transform spawnTransform;

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
        spawnTransform = ParentPosition;
        NewObject = NewPlace;
    }

    public void ChangeFarm(int spawnIndex)
    {
        NewPlace = Instantiate(farmPrefabs[spawnIndex], spawnTransform.position, spawnTransform.rotation);
        Destroy(emptyFarm);

        GetComponent<Receptacle>().enabled = false;
    }
}


