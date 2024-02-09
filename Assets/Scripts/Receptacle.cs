using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receptacle : MonoBehaviour
{
    [SerializeField] private Collider gettingCollider; // �����, � ������� ����� ������. ���� ���� ������� ������������
    [SerializeField] private GameObject currentEmptyFarm;
    [SerializeField] private List<GameObject> farmPrefabs;
    private Transform spawnTransform;
    private Receptacle receptacle;
    private void Start() {
        receptacle = GetComponent<Receptacle>();
    }
   
    public void ChangeFarm(int spawnIndex)
    {
        spawnTransform = currentEmptyFarm.transform;
        Instantiate(farmPrefabs[spawnIndex], spawnTransform.position, spawnTransform.rotation);
        Destroy(currentEmptyFarm);

        receptacle.enabled = false; // ����������� �������
    }
}
