using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Receptacle : MonoBehaviour
{
    [SerializeField] private Collider gettingCollider; // �����, � ������� ����� ������. ���� ���� ������� ������������
    [SerializeField] private GameObject currentEmptyFarm;
    [SerializeField] private List<GameObject> farmPrefabs;
    private Transform spawnTransform;
    [SerializeField] private MovingBetween broadcastScript;
    public Dictionary<int, GameObject> DictPlaces = new();
    public int IndexPlace = 1;

    private void Start()
    {
        DictPlaces = broadcastScript.DictPlaces;
        IndexPlace = PlayerPrefs.GetInt("IndexPlace");
        Debug.Log(IndexPlace);
    }
    public void ChangeFarm(int spawnIndex)
    {
        spawnTransform = currentEmptyFarm.transform;
        Instantiate(farmPrefabs[spawnIndex], spawnTransform.position, spawnTransform.rotation);
        DictPlaces[IndexPlace] = farmPrefabs[spawnIndex];
        Destroy(currentEmptyFarm);

        GetComponent<Receptacle>().enabled = false; // ����������� �������
    }
}
