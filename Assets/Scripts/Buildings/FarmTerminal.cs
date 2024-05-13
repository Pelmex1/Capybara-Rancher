using UnityEngine;
using CapybaraRancher.EventBus;

public class FarmTerminal : MonoBehaviour
{
    [SerializeField] private Transform spawnPos;
    private bool _isNear = false;
    private bool[] _isBuy;
    private FarmObject[] _farmObjects;
    private GameObject[] _spawnedGameObjects;

    private void Start()
    {
        _farmObjects = EventBus.GetBuildings();
        _spawnedGameObjects = new GameObject[_farmObjects.Length];
        _isBuy = new bool[_farmObjects.Length];
        for (int i = 0; i < _farmObjects.Length; i++)
        {
            _spawnedGameObjects[i] = Instantiate(_farmObjects[i].Prefab, spawnPos.position, Quaternion.identity, gameObject.transform);
            if (PlayerPrefs.GetString($"{name}_{_spawnedGameObjects[i].name}", "false") == "true")
            {
                _spawnedGameObjects[i].SetActive(true);
                _isBuy[i] = true;
            }
            else
            {
                _spawnedGameObjects[i].SetActive(false);
            }
        }

    }

    private void Update()
    {
        if (_isNear)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Cursor.lockState = CursorLockMode.Confined;
                EventBus.ActiveFarmPanel(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventBus.ActiveHelpText(true);
            _isNear = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventBus.ActiveHelpText(false);
            EventBus.ActiveFarmPanel(false);
            _isNear = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    private void BuyOrRemove(int index, bool isEnable)
    {
        if (_isNear)
        {
            _spawnedGameObjects[index].SetActive(isEnable);
            _isBuy[index] = isEnable;
        }
    }
    private void OnEnable()
    {
        EventBus.BuyFarm += BuyOrRemove;
    }
    private void OnDisable()
    {
        EventBus.BuyFarm -= BuyOrRemove;
    }
    private void OnApplicationQuit()
    {
        for (int i = 0; i < _farmObjects.Length; i++)
        {
            if (_isBuy[i])
            {
                PlayerPrefs.SetString($"{name}_{_spawnedGameObjects[i].name}", "true");
            }
        }

    }
}