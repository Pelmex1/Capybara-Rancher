using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receptacle : MonoBehaviour
{
    [SerializeField] private Collider gettingCollider; // Штука, в которую нужно кидать. Типо сама воронка засасывающая
    [SerializeField] private GameObject currentEmptyFarm;
    [SerializeField] private List<GameObject> farmPrefabs;
    private Transform spawnTransform;
   
    public void ChangeFarm(int spawnIndex)
    {
        spawnTransform = currentEmptyFarm.transform;
        Instantiate(farmPrefabs[spawnIndex], spawnTransform.position, spawnTransform.rotation);
        Destroy(currentEmptyFarm);

        GetComponent<Receptacle>().enabled = false; // Одноразовая функция
    }
}
