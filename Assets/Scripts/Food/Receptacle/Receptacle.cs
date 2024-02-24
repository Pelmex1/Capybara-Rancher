using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Receptacle : MonoBehaviour
{
    [SerializeField] private Collider gettingCollider; // �����, � ������� ����� ������. ���� ���� ������� ������������
    [SerializeField] private GameObject currentEmptyFarm;
    [SerializeField] private List<GameObject> farmPrefabs;
    private Transform spawnTransform;
    public UIBilding UIBuilding;

    public void ChangeFarm(int spawnIndex)
    {
        spawnTransform = UIBuilding.ParentPlace.transform;
        UIBuilding.NewObject = Instantiate(farmPrefabs[spawnIndex], spawnTransform.position, spawnTransform.rotation);
        Destroy(currentEmptyFarm);

        GetComponent<Receptacle>().enabled = false; // ����������� �������
    }
}
