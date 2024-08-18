using UnityEngine;
using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;

public class FarmTerminal : MonoBehaviour
{
    [SerializeField] private Transform spawnPos;
    private bool _isNear = false;
    private bool[] _isBuy;
    private FarmObject[] _farmObjects;
    private GameObject[] _spawnedGameObjects;
    private bool _isAnyBuy = false;
    private IUpgradeManager[] _upgrades;

    private void Start()
    {
        _farmObjects = EventBus.GetBuildings();
        _spawnedGameObjects = new GameObject[_farmObjects.Length];
        _upgrades = new IUpgradeManager[_farmObjects.Length];
        _isBuy = new bool[_farmObjects.Length];
        for (int i = 0; i < _farmObjects.Length; i++)
        {
            _spawnedGameObjects[i] = Instantiate(_farmObjects[i].Prefab, spawnPos.position, Quaternion.identity, gameObject.transform);
            _spawnedGameObjects[i].transform.position = spawnPos.position;
            _upgrades[i] = _spawnedGameObjects[i].GetComponent<IUpgradeManager>();
            if (PlayerPrefs.GetString($"{transform.parent.transform.parent.name}_{_spawnedGameObjects[i].name}", "false") == "true")
            {
                _spawnedGameObjects[i].SetActive(true);
                _isBuy[i] = true;
                _isAnyBuy = true;
                spawnPos.gameObject.SetActive(false);
            }
            else
            {
                _spawnedGameObjects[i].SetActive(false);
            }
        }

    }
    private void Upgrade(int index)
    {
        if(_isNear)
        {
            _upgrades[index].UpUpgrade();
        }
    }
    private void EventUpdate()
    {
        if (_isNear)
        {
            Cursor.lockState = CursorLockMode.Confined;
            EventBus.ActiveFarmPanel(true);
            EventBus.UpdateFarmButtons(_isBuy, _isAnyBuy);
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
        }
    }
    private void BuyOrRemove(int index, bool isEnable)
    {
        if (_isNear)
        {
            _spawnedGameObjects[index].SetActive(isEnable);
            _isBuy[index] = isEnable;
            _isAnyBuy = isEnable;
            spawnPos.gameObject.SetActive(!isEnable);
            EventBus.UpdateFarmButtons(_isBuy, _isAnyBuy);
        }
    }
    private void OnEnable()
    {
        EventBus.TerminalUseInput += EventUpdate;
        EventBus.BuyFarm += BuyOrRemove;
        EventBus.GlobalSave += Save;
        EventBus.SentUpgrade += Upgrade;
    }
    private void OnDisable()
    {
        EventBus.TerminalUseInput -= EventUpdate;
        EventBus.BuyFarm -= BuyOrRemove;
        EventBus.GlobalSave -= Save;
        EventBus.SentUpgrade -= Upgrade;
    }
    private void OnApplicationQuit()
    {
        Save();
    }
    private void Save()
    {
        for (int i = 0; i < _farmObjects.Length; i++)
        {
            if (_isBuy[i])
            {
                PlayerPrefs.SetString($"{transform.parent.transform.parent.name}_{_spawnedGameObjects[i].name}", "true");
            }
        }
    }
}