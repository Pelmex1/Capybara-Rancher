using System.Collections.Generic;
using UnityEngine;

public class Receptacle : MonoBehaviour
{
    [SerializeField] private Collider gettingCollider;
    [SerializeField] private GameObject emptyFarm;
    [SerializeField] private List<GameObject> farmPrefabs;
    
    public UIBilding UIBuilding;

    private Transform spawnTransform;

    private void Start()
    {
        UIBuilding = transform.GetComponentInParent<UIBilding>();
    }

    public void ChangeFarm(int spawnIndex)
    {
        spawnTransform = UIBuilding.ParentPlace.transform;
        UIBuilding.NewObject = Instantiate(farmPrefabs[spawnIndex], spawnTransform.position, spawnTransform.rotation, UIBuilding.ParentPosition);
        Destroy(emptyFarm);

        GetComponent<Receptacle>().enabled = false;
    }
}
