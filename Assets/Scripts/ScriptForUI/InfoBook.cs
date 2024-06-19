using CapybaraRancher.Enums;
using CapybaraRancher.EventBus;
using DevionGames;
using UnityEngine;
using UnityEngine.UI;

public class InfoBook : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _lore;
    [SerializeField] private GameObject _tasks;
    [SerializeField] private GameObject _orders;
    [SerializeField] private Sprite _completedTaskSprite;

    [HeaderLine("Tasks")]
    [SerializeField] private Image[] _taskPoints;
    [SerializeField] private GameObject[] _prompts;

    [HeaderLine("Orders")]
    [SerializeField] private float[] _ordersCount;
    [SerializeField] private Image[] _ordersBars;
    [SerializeField] private float[] _ordersRewards;
    [SerializeField] private Image[] _ordersPoints;
    [SerializeField] private Image _fineBar;
    [SerializeField] private float _fineTime;
    [SerializeField] private float _fineCount;
    private bool[] _ordersComplete;

    private void OnEnable()
    {
        EventBus.PauseInput += PauseInput;
        EventBus.InfoBookInput += InfoBookInput;
        EventBus.AddInPool += CompleteTask;
        EventBus.Win += CompleteAllTask;
        EventBus.SelledCrystal += CompleteSell;
    }

    private void OnDisable()
    {
        EventBus.PauseInput -= PauseInput;
        EventBus.InfoBookInput -= InfoBookInput;
        EventBus.AddInPool -= CompleteTask;
        EventBus.Win -= CompleteAllTask;
        EventBus.SelledCrystal -= CompleteSell;
    }

    private void Start()
    {
        _ordersComplete = new bool[_ordersBars.Length];
        _ordersBars[0].fillAmount = PlayerPrefs.GetFloat("Order0", 0f);
        _ordersBars[1].fillAmount = PlayerPrefs.GetFloat("Order1", 0f);
        _ordersBars[2].fillAmount = PlayerPrefs.GetFloat("Order2", 0f);
    }
    private void InfoBookInput(){
        if (Time.timeScale != 0)
        {
            _panel.SetActive(!_panel.activeSelf);
            Cursor.lockState = _panel.activeSelf == false ? CursorLockMode.Locked : CursorLockMode.Confined;
        }
    }
    private void PauseInput(){
        _panel.SetActive(false);
    }

    private void LateUpdate()
    {
        _fineBar.fillAmount += Time.deltaTime / _fineTime;
        if (_fineBar.fillAmount == 1f)
        {
            _fineBar.fillAmount = 0f;
            EventBus.AddMoney(-1f * _fineCount);
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("Order0", _ordersBars[0].fillAmount);
        PlayerPrefs.SetFloat("Order1", _ordersBars[1].fillAmount);
        PlayerPrefs.SetFloat("Order2", _ordersBars[2].fillAmount);
    }

    private void CompleteTask(GameObject needlessVariable, TypeGameObject taskNumber)
    {
        switch (taskNumber)
        {
            case TypeGameObject.FirstPart : _taskPoints[0].sprite = _completedTaskSprite; break;
            case TypeGameObject.SecondPart : _taskPoints[1].sprite = _completedTaskSprite; break;
            case TypeGameObject.ThirdPart : _taskPoints[2].sprite = _completedTaskSprite; break;
            default: break;
        }
    }

    private void CompleteAllTask()
    {
        foreach (Image point in _taskPoints)
            point.sprite = _completedTaskSprite;
    }
    private void CompleteSell(TypeGameObject crystalType)
    {
        switch (crystalType)
        {
            case TypeGameObject.GladeCrystal : if (!_ordersComplete[0]) {
                    _ordersBars[0].fillAmount += 1f / _ordersCount[0];
                    PlayerPrefs.SetFloat("Order0", _ordersBars[0].fillAmount);
                } break;
            case TypeGameObject.RockCrystal: if (!_ordersComplete[1]) {
                    PlayerPrefs.SetFloat("Order1", _ordersBars[1].fillAmount);
                    _ordersBars[1].fillAmount += 1f / _ordersCount[1];
                } break;
            case TypeGameObject.LeoCrystal: if (!_ordersComplete[2]) {
                    PlayerPrefs.SetFloat("Order2", _ordersBars[2].fillAmount);
                    _ordersBars[2].fillAmount += 1f / _ordersCount[2];
                } break;
            default: break;
        }
        _fineBar.fillAmount = 0f;
        CheckRewards();
    }

    private void CheckRewards()
    {
        for (int i = 0; i < _ordersRewards.Length; i++)
        {
            if (_ordersBars[i].fillAmount == 1f)
            {
                EventBus.AddMoney.Invoke(_ordersRewards[i]);
                _ordersPoints[i].sprite = _completedTaskSprite;
                _ordersComplete[i] = true;
            }
        }
    }

    public void Prompt(int promptNumber)
    {
        for (int i = 0; i < _prompts.Length; i++)
        {
            if (!_prompts[promptNumber].activeSelf && promptNumber == i)
            {
                _prompts[promptNumber].SetActive(true);
                continue;
            }
            _prompts[i].SetActive(false);
        }
    }

    public void LorePage()
    {
        _lore.SetActive(true);
        _orders.SetActive(false);
        _tasks.SetActive(false);
    }

    public void TasksPage()
    {
        _tasks.SetActive(true);
        _lore.SetActive(false);
        _orders.SetActive(false);
    }
    public void OrdersPage()
    {
        _orders.SetActive(true);
        _tasks.SetActive(false);
        _lore.SetActive(false);
    }
}
