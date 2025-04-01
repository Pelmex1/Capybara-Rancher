using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using CustomEventBus;
using CapybaraRancher.Consts;
using CapybaraRancher.Abstraction.Signals.UI;
using CapybaraRancher.Abstraction.Signals;
using CapybaraRancher.Abstraction.Signals.Input;

public class LocalMenu : MonoBehaviour
{

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
    private EventBus _eventBus;

    private void LateUpdate()
    {
        UpdateBars();
    }
    private void EventUpdate(IPauseInput pauseInput){
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
    private void OnMoneyChanged(IOnMoneyChanged OnMoneyChangedClass)
    {
        _money.text = $"{OnMoneyChangedClass.Money}";
    }
    private void OnEnable()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<IOnMoneyChanged>(OnMoneyChanged);
        _eventBus.Subscribe<IPauseInput>(EventUpdate);
        EventBus.GetEnergyPlayerData += GetEnergy;
        EventBus.GiveEnergyPlayerData += GiveEnergyData;
    }

    private void OnDisable()
    {
        _eventBus.UnSubscribe<IPauseInput>(EventUpdate);
        _eventBus.UnSubscribe<IOnMoneyChanged>(OnMoneyChanged);
        EventBus.GetEnergyPlayerData -= GetEnergy;
        EventBus.GiveEnergyPlayerData -= GiveEnergyData;
    }

    private void GetEnergy()
    {
        _energyMaxValue = PlayerPrefs.GetFloat(Constants.ENERGY_KEY);
        _hpMaxValue = PlayerPrefs.GetFloat(Constants.HEALTH_KEY);
        _hungerMaxValue = PlayerPrefs.GetFloat(Constants.HUNGER_KEY);
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
        _eventBus.Invoke<IGlobalSave>(new());
        SceneManager.LoadScene(Constants.NAME_MAIN_MENU);
    } 
}
