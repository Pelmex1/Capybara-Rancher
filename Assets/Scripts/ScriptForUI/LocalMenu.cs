using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using CapybaraRancher.EventBus;
using CapybaraRancher.Enums;

public class LocalMenu : MonoBehaviour
{
    private const string ENERGY_KEY = "EnergyMaxValue";
    private const string HEALTH_KEY = "HealthMaxValue";
    private const string HUNGER_KEY = "HungerMaxValue";

    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _panelOptions;
    [SerializeField] private Image _energyBar;
    [SerializeField] private Image _hpBar;
    [SerializeField] private Image _hungerBar;
    [SerializeField] private TMP_Text _money;

    private float _energyMaxValue;
    private float _energyCurrentValue;
    private float _hpMaxValue;
    private float _hpCurrentValue;
    private float _hungerMaxValue;
    private float _hungerCurrentValue;

    private void Update()
    {
        float money = EventBus.GetMoney.Invoke();
        _money.text = $"{money}";
        if (InputManager.Instance.IsActionDown(ActionType.Pause))
        {
            if (!_pausePanel.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Confined;
                _pausePanel.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                _pausePanel.SetActive(false);
                _panelOptions.SetActive(false);
                Time.timeScale = 1;
            }
        }
        UpdateBars();
    }

    private void OnEnable()
    {
        EventBus.GetEnergyPlayerData += GetEnergy;
        EventBus.GiveEnergyPlayerData += GiveEnergyData;
    }

    private void OnDisable()
    {
        EventBus.GetEnergyPlayerData += GetEnergy;
        EventBus.GiveEnergyPlayerData += GiveEnergyData;
    }

    private void GetEnergy()
    {
        _energyMaxValue = PlayerPrefs.GetFloat(ENERGY_KEY);
        _hpMaxValue = PlayerPrefs.GetFloat(HEALTH_KEY);
        _hungerMaxValue = PlayerPrefs.GetFloat(HUNGER_KEY);
    }

    private void GiveEnergyData(float Hp, float Energy, float Hunger)
    {
        _hpCurrentValue = Hp;
        _energyCurrentValue = Energy;
        _hungerCurrentValue = Hunger;
    }

    private void UpdateBars()
    {
        _energyBar.fillAmount = (_energyCurrentValue - 5) / (_energyMaxValue - 5);
        _hpBar.fillAmount = _hpCurrentValue / _hpMaxValue;
        _hungerBar.fillAmount = _hungerCurrentValue / _hungerMaxValue;
    }
    public void OnOptions() => _panelOptions.SetActive(true);
    public void OffOptions() => _panelOptions.SetActive(false);

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void SaveQuit()
    {
        EventBus.GlobalSave.Invoke();
        SceneManager.LoadScene("MainMenu");
    } 
}
