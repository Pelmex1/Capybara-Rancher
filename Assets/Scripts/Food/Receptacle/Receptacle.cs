using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;
using UnityEngine;

public class Receptacle : MonoBehaviour
{
    [SerializeField] private GameObject _farm;
    [SerializeField] private Collider _localCollider;
    private const string MOVEBLETAG = "movebleObject";
    private FarmType[] _farms;
    private GameObject[] _spawnedFarms;
    private int _index = -1;

    private void Start() {
        _localCollider = GetComponent<Collider>();
        _farms = EventBus.GetFarms();
        _spawnedFarms = new GameObject[_farms.Length];
        for(int i = 0; i < _farms.Length; i++){
            _spawnedFarms[i] = Instantiate(_farms[i].farm,_farm.transform.position,Quaternion.identity,transform);
            _spawnedFarms[i].SetActive(false);
        }
        _index = PlayerPrefs.GetInt("FarmIndex", -1);
        if(_index != -1){
            _localCollider.enabled = false;
            _spawnedFarms[_index].SetActive(false);
            _farm.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag(MOVEBLETAG))
        {
            if(other.gameObject.TryGetComponent(out IMovebleObject type))
            {
                for(int i = 0; i < _farms.Length; i++)
                {
                    if(_farms[i].type == type.Data.TypeGameObject)
                    {
                        _localCollider.enabled = false;
                        _spawnedFarms[i].SetActive(true);
                        _farm.SetActive(false);
                        other.gameObject.SetActive(false);
                        _index = i;
                    }
                }
            }
        }
    }
    private void OnDisable() {
        if(_index != -1){
            _localCollider.enabled = true;
            _spawnedFarms[_index].SetActive(false);
            _index = -1;
        }
    }
    private void OnApplicationQuit() {
        PlayerPrefs.SetInt("FarmIndex", _index);
    }
}


