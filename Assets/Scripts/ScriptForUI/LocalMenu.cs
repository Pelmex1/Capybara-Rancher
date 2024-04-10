using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CustomEventBus;

public class LocalMenu : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject PanelOptions;
    [SerializeField] private Image energyBar;
    [SerializeField] private Image hpBar;

    private int indexCheck = 0;
    private float energyMaxValue;
    private float energyCurrentValue;
    private float hpMaxValue;
    private float hpCurrentValue;

    private void Update()
    {
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

    private void GetEnergy(float EnergyMaxValue, float HpMaxValue)
    {
        energyMaxValue = EnergyMaxValue;
        hpMaxValue = HpMaxValue;
    }

    private void GiveEnergyData(float Hp, float Energy)
    {
        hpCurrentValue = Hp;
        energyCurrentValue = Energy;
    }

    private void UpdateBars()
    {
        energyBar.fillAmount = (energyCurrentValue - 5) / (energyMaxValue - 5);
        hpBar.fillAmount = hpCurrentValue / hpMaxValue;
    }
    public void OnOptions() => PanelOptions.SetActive(true);
    public void OffOptions() => PanelOptions.SetActive(false);

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void SaveQuit() => SceneManager.LoadScene("MainMenu");
}
