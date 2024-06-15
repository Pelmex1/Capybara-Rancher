using CapybaraRancher.Enums;
using CapybaraRancher.EventBus;
using UnityEngine;
using UnityEngine.UI;

public class InfoBook : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _lore;
    [SerializeField] private GameObject _tasks;
    [SerializeField] private Image[] _taskPoints;
    [SerializeField] private GameObject[] _prompts;
    [SerializeField] private Sprite _completedTaskSprite;

    private void OnEnable()
    {
        EventBus.AddInPool += CompleteTask;
        EventBus.Win += CompleteAllTask;
    }

    private void OnDisable()
    {
        EventBus.AddInPool -= CompleteTask;
        EventBus.Win -= CompleteAllTask;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && Time.timeScale != 0)
        {
            _panel.SetActive(!_panel.activeSelf);
            Cursor.lockState = _panel.activeSelf == false ? CursorLockMode.Locked : CursorLockMode.Confined;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
            _panel.SetActive(false);
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
        foreach (var point in _taskPoints)
            point.sprite = _completedTaskSprite;
    }

    public void LorePage()
    {
        _lore.SetActive(true);
        _tasks.SetActive(false);
    }

    public void TasksPage()
    {
        _tasks.SetActive(true);
        _lore.SetActive(false);
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
}
