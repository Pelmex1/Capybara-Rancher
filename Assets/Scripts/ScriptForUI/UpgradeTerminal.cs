using UnityEngine;
using CapybaraRancher.EventBus;
using TMPro;

public class UpgradeTerminal : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    private const string INFOTEXT_TEXT = "Press Key E";
    private const float VALUE_UPGRADE = 2f;
    private const string ENERGY_KEY = "Energy";
    private const float ENERGYMAXVALUE_UPGRADE_PRICE = 300f;
    private const string HEALTH_KEY = "Health";
    private const float HEALTHMAXVALUE_UPGRADE_PRICE = 150f;
    private const string HUNGER_KEY = "Hunger";
    private const float HUNGERMAXVALUE_UPGRADE_PRICE = 200f;

    [SerializeField] private TMP_Text InfoText;
    [SerializeField] private GameObject _terminalPanel;
    [SerializeField] private GameObject[] _infoPanels;

    private bool isNear = false;

    private void Update()
    {
        if (isNear)
            OnUi();
        if (Input.GetKeyDown(KeyCode.K))
        {
            ClearUpgradeSaves();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            InfoText.text = INFOTEXT_TEXT;
            isNear = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            InfoText.text = "";
            isNear = false;
            _terminalPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void OnUi()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _terminalPanel.SetActive(!_terminalPanel.activeSelf);
            Cursor.lockState = _terminalPanel.activeSelf ? CursorLockMode.Confined : CursorLockMode.Locked;
            InfoText.text = "";
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            _terminalPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void MaxValueUpgrade(string parametr)
    {
        if(PlayerPrefs.GetInt(parametr + "MaxValueUpgrade", 0) == 0)
        {
            float price;
            switch (parametr)
            {
                case ENERGY_KEY: price = ENERGYMAXVALUE_UPGRADE_PRICE; break;
                case HEALTH_KEY: price = HEALTHMAXVALUE_UPGRADE_PRICE; break;
                case HUNGER_KEY: price = HUNGERMAXVALUE_UPGRADE_PRICE; break;
                default: price = 0; break;
            }
            EventBus.AddMoney(-1f * price);
            float newValue = PlayerPrefs.GetFloat(parametr + "MaxValue") * VALUE_UPGRADE;
            PlayerPrefs.SetFloat(parametr + "MaxValue", newValue);
            PlayerPrefs.SetInt(parametr + "MaxValueUpgrade", 1);
            EventBus.MaxValueUpgrade.Invoke();
            EventBus.GetEnergyPlayerData.Invoke();
        }
    }

    private void ClearUpgradeSaves() // for tests
    {
        PlayerPrefs.DeleteKey("EnergyMaxValue");
        PlayerPrefs.DeleteKey("HealthMaxValue");
        PlayerPrefs.DeleteKey("HungerMaxValue");
        PlayerPrefs.DeleteKey("EnergyMaxValueUpgrade");
        PlayerPrefs.DeleteKey("HealthMaxValueUpgrade");
        PlayerPrefs.DeleteKey("HungerMaxValueUpgrade");
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
}
