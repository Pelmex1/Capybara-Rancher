using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using CapybaraRancher.EventBus;

public class LocalMenu : MonoBehaviour
{
    private const string ENERGY_KEY = "EnergyMaxValue";
    private const string HEALTH_KEY = "HealthMaxValue";
    private const string HUNGER_KEY = "HungerMaxValue";

    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject PanelOptions;
    [SerializeField] private Image energyBar;
    [SerializeField] private Image hungerBar;
    [SerializeField] private Image hpBar;
    [SerializeField] private TMP_Text _money;

    private int indexCheck = 0;
    private float energyMaxValue;
    private float energyCurrentValue;
    private float hpMaxValue;
    private float hpCurrentValue;
    private float hungerMaxValue;
    private float hungerCurrentValue;

    private void Update()
    {
        float money = EventBus.GetMoney.Invoke();
        _money.text = $"{money}";
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (indexCheck == 0)
            {
                Cursor.lockState = CursorLockMode.Confined;
                PausePanel.SetActive(true);
                Time.timeScale = 0;
                indexCheck = 1;
            }
            else if (indexCheck == 1)
            {
                Cursor.lockState = CursorLockMode.Locked;
                PausePanel.SetActive(false);
                Time.timeScale = 1;
                indexCheck = 0;
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
        energyMaxValue = PlayerPrefs.GetFloat(ENERGY_KEY);
        hpMaxValue = PlayerPrefs.GetFloat(HEALTH_KEY);
        hungerMaxValue = PlayerPrefs.GetFloat(HUNGER_KEY);
    }

    private void GiveEnergyData(float Hp, float Energy, float Hunger)
    {
        hpCurrentValue = Hp;
        energyCurrentValue = Energy;
        hungerCurrentValue = Hunger;
    }

    private void UpdateBars()
    {
        energyBar.fillAmount = (energyCurrentValue - 5) / (energyMaxValue - 5);
        hpBar.fillAmount = hpCurrentValue / hpMaxValue;
        hungerBar.fillAmount = hungerCurrentValue / hungerMaxValue;
    }
    public void OnOptions() => PanelOptions.SetActive(true);
    public void OffOptions() => PanelOptions.SetActive(false);

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void SaveQuit()
    {
        SceneManager.LoadScene("MainMenu");
    } 
}
