using CapybaraRancher.Consts;
using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class Receptacle : MonoBehaviour
{
    [SerializeField] private GameObject _farm;
    [SerializeField] private Collider _localCollider;
    [SerializeField] private Image image;
    private FarmType[] _farms;
    private GameObject[] _spawnedFarms;
    private int _index = 0;

    private void Start() {
        _localCollider = GetComponent<Collider>();
        _farms = EventBus.GetFarms();
        _spawnedFarms = new GameObject[_farms.Length];
        for(int i = 0; i < _farms.Length; i++){
            _spawnedFarms[i] = Instantiate(_farms[i].farm,_farm.transform.position,Quaternion.identity,transform);
            _spawnedFarms[i].SetActive(false);
        }
        _index = _index != -1 ? PlayerPrefs.GetInt($"{transform.parent.transform.parent.transform.parent.transform.parent.name}_FarmIndex", -1) : -1;
        if(_index != -1){
            _localCollider.enabled = false;
            _spawnedFarms[_index].SetActive(true);
            _farm.SetActive(false);
            image.sprite = _farms[_index].sprite;
            image.enabled = true;
        }
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag(Constants.MOVEBLE_OBJECT_TAG))
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
                        image.sprite = _farms[i].sprite;
                        image.enabled = true;
                    }
                }
            }
        }
    }
    private void OnEnable() {
        if(_index == -1){
            _farm.SetActive(true);
        }
    }
    private void OnDisable() {
        if(_index != -1 && _farms != null){
            _localCollider.enabled = true;
            _spawnedFarms[_index].SetActive(false);
            _index = -1;
            image.enabled = false;
        }
    }
    private void OnApplicationQuit() {
        PlayerPrefs.SetInt($"{transform.parent.transform.parent.transform.parent.transform.parent.name}_FarmIndex", _index);
    }
}