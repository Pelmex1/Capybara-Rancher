using UnityEngine;
using CapybaraRancher.EventBus;
using CapybaraRancher.Enums;

public class UpgradeTerminal : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    private const float VALUE_UPGRADE = 2f;
    private const string ENERGY_KEY = "Energy";
    private const float ENERGYMAXVALUE_UPGRADE_PRICE = 100f;
    private const string HEALTH_KEY = "Health";
    private const float HEALTHMAXVALUE_UPGRADE_PRICE = 50f;
    private const string HUNGER_KEY = "Hunger";
    private const float HUNGERMAXVALUE_UPGRADE_PRICE = 75f;
    private const string EXTRASLOT_KEY = "ExtraSlotUpgrade";
    private const float EXTRASLOT_VALUE_UPGRADE_PRICE = 125f;
    private const string ENERGY_SPENDING_KEY = "EnergySpendingRate";
    private const float ENERGY_SPENDING_PRICE = 105f;
    private const float ENERGY_SPENDING_RATE = 5f;

    [SerializeField] private GameObject InfoText;
    [SerializeField] private GameObject _terminalPanel;
    [SerializeField] private GameObject[] _infoPanels;

    private bool isNear = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            InfoText.SetActive(true);
            isNear = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            InfoText.SetActive(false);
            isNear = false;
            _terminalPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    private void Pause() => _terminalPanel.SetActive(false);
    private void Use()
    {
        if (isNear && Time.timeScale != 0)
        {
            _terminalPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            InfoText.SetActive(false);
        }
    }

    public void MaxValueUpgrade(string parametr)
    {
        if (PlayerPrefs.GetInt(parametr + "MaxValueUpgrade", 0) == 0)
        {
            var price = parametr switch
            {
                ENERGY_KEY => ENERGYMAXVALUE_UPGRADE_PRICE,
                HEALTH_KEY => HEALTHMAXVALUE_UPGRADE_PRICE,
                HUNGER_KEY => HUNGERMAXVALUE_UPGRADE_PRICE,
                _ => 0,
            };
            if (EventBus.GetMoney() >= price)
            {
                EventBus.AddMoney(-price);
                float newValue = PlayerPrefs.GetFloat(parametr + "MaxValue") * VALUE_UPGRADE;
                PlayerPrefs.SetFloat(parametr + "MaxValue", newValue);
                PlayerPrefs.SetInt(parametr + "MaxValueUpgrade", 1);
                EventBus.MaxValueUpgrade.Invoke();
                EventBus.GetEnergyPlayerData.Invoke();
            }
        }
    }

    public void ExtraSlotUpgrade()
    {
        if (PlayerPrefs.GetInt(EXTRASLOT_KEY, 0) == 0)
        {
            if (EventBus.GetMoney() >= EXTRASLOT_VALUE_UPGRADE_PRICE)
            {
                EventBus.AddMoney(-EXTRASLOT_VALUE_UPGRADE_PRICE);
                PlayerPrefs.SetInt(EXTRASLOT_KEY, 1);
                EventBus.ExtraSlotUpgrade.Invoke();
            }
        }
    }

    public void EnergySpendingUpgrade()
    {
        if (PlayerPrefs.GetFloat(ENERGY_SPENDING_KEY, 0) != ENERGY_SPENDING_RATE)
        {
            if (EventBus.GetMoney() >= ENERGY_SPENDING_PRICE)
            {
                EventBus.AddMoney(-ENERGY_SPENDING_PRICE);
                PlayerPrefs.SetFloat(ENERGY_SPENDING_KEY, ENERGY_SPENDING_RATE);
                EventBus.EnergySpendingUpgrade.Invoke();
            }
        }
    }

    public void InfoPanelActivate(int numberOfPanel)
    {
        foreach (GameObject element in _infoPanels)
            element.SetActive(false);
        _infoPanels[numberOfPanel].SetActive(true);
    }

    public void TerminalExit()
    {
        _terminalPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnEnable() {
        EventBus.PauseInput += Pause;
        EventBus.TerminalUseInput += Use;
    }
    private void OnDisable() {
        EventBus.PauseInput -= Pause;
        EventBus.TerminalUseInput -= Use;
    }
}