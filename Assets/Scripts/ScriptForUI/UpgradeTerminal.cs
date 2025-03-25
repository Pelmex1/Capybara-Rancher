using UnityEngine;
using CapybaraRancher.EventBus;
using TMPro;
using CapybaraRancher.Consts;

public class UpgradeTerminal : MonoBehaviour
{


    [SerializeField] private GameObject InfoText;
    [SerializeField] private GameObject _terminalPanel;
    [SerializeField] private GameObject[] _infoPanels;
    [SerializeField] private TextMeshProUGUI[] _buyTexts;

    private bool isNear = false;
    private void Start()
    {
        if (PlayerPrefs.GetInt(Constants.ENERGY_KEY + "MaxValueUpgrade", 0) != 0)
            _buyTexts[0].text = Constants.SOLD_TEXT;
        if (PlayerPrefs.GetInt(Constants.HEALTH_KEY + "MaxValueUpgrade", 0) != 0)
            _buyTexts[1].text = Constants.SOLD_TEXT;
        if (PlayerPrefs.GetInt(Constants.HUNGER_KEY + "MaxValueUpgrade", 0) != 0)
            _buyTexts[2].text = Constants.SOLD_TEXT;
        if (PlayerPrefs.GetInt(Constants.EXTRASLOT_KEY, 0) != 0)
            _buyTexts[3].text = Constants.SOLD_TEXT;
        if (PlayerPrefs.GetFloat(Constants.ENERGY_SPENDING_KEY, 0) == Constants.ENERGY_SPENDING_RATE)
            _buyTexts[4].text = Constants.SOLD_TEXT;
        if (!(PlayerPrefs.GetString(Constants.SATCHEL_KEY, "false") == "false"))
            _buyTexts[5].text = Constants.SOLD_TEXT;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.PLAYER_TAG))
        {
            InfoText.SetActive(true);
            isNear = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.PLAYER_TAG))
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
            float price = parametr switch
            {
                Constants.ENERGY_KEY => Constants.ENERGYMAXVALUE_UPGRADE_PRICE,
                Constants.HEALTH_KEY => Constants.HEALTHMAXVALUE_UPGRADE_PRICE,
                Constants.HUNGER_KEY => Constants.HUNGERMAXVALUE_UPGRADE_PRICE,
                _ => 0,
            };
            int number = parametr switch
            {
                Constants.ENERGY_KEY => 0,
                Constants.HEALTH_KEY => 1,
                Constants.HUNGER_KEY => 2,
                _ => 0,
            };
            if (EventBus.GetMoney() >= price)
            {
                _buyTexts[number].text = Constants.SOLD_TEXT;
                EventBus.AddMoney(-price);
                float newValue = PlayerPrefs.GetFloat(parametr + "MaxValue") * Constants.VALUE_UPGRADE;
                PlayerPrefs.SetFloat(parametr + "MaxValue", newValue);
                PlayerPrefs.SetInt(parametr + "MaxValueUpgrade", 1);
                EventBus.MaxValueUpgrade.Invoke();
                EventBus.GetEnergyPlayerData.Invoke();
            }
        }
    }

    public void ExtraSlotUpgrade()
    {
        if (PlayerPrefs.GetInt(Constants.EXTRASLOT_KEY, 0) == 0)
        {
            if (EventBus.GetMoney() >= Constants.EXTRASLOT_VALUE_UPGRADE_PRICE)
            {
                _buyTexts[3].text = Constants.SOLD_TEXT;
                EventBus.AddMoney(-Constants.EXTRASLOT_VALUE_UPGRADE_PRICE);
                PlayerPrefs.SetInt(Constants.EXTRASLOT_KEY, 1);
                EventBus.ExtraSlotUpgrade.Invoke();
            }
        }
    }

    public void EnergySpendingUpgrade()
    {
        if (PlayerPrefs.GetFloat(Constants.ENERGY_SPENDING_KEY, 0) != Constants.ENERGY_SPENDING_RATE)
        {
            if (EventBus.GetMoney() >= Constants.ENERGY_SPENDING_PRICE)
            {
                _buyTexts[4].text = Constants.SOLD_TEXT;
                EventBus.AddMoney(-Constants.ENERGY_SPENDING_PRICE);
                PlayerPrefs.SetFloat(Constants.ENERGY_SPENDING_KEY, Constants.ENERGY_SPENDING_RATE);
                EventBus.EnergySpendingUpgrade.Invoke();
            }
        }
    }
    public void BuySatchel()
    {
        if (PlayerPrefs.GetString(Constants.SATCHEL_KEY, "false") == "false")
        {
            if (EventBus.GetMoney() >= Constants.SATCHEL_VALUE_UPGRADE_PRICE)
            {
                _buyTexts[5].text = Constants.SOLD_TEXT;
                EventBus.AddMoney(-Constants.SATCHEL_VALUE_UPGRADE_PRICE);
                PlayerPrefs.SetString(Constants.SATCHEL_KEY, "true");
                EventBus.BuyJump(true);
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