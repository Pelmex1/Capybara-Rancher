using UnityEngine;
using CapybaraRancher.EventBus;
using System.Collections;
using CapybaraRancher.Consts;

public class TutorialController : MonoBehaviour
{


    [SerializeField] private GameObject _tutorialPanel;
    [SerializeField] private GameObject _askToTutorPanel;
    [SerializeField] private GameObject[] _tips;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("TutorialComplete", 0) == 0)
        {
            if (EventBus.GetMoney() < 300f)
                EventBus.AddMoney(Constants.START_MONEY);
            _tutorialPanel.SetActive(true);
            _askToTutorPanel.SetActive(true);
            _tips[0].SetActive(true);


        }
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("TutorialComplete", 0) == 0)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;
        }
    }
    private void EventUpdate()
    {
        if (Time.timeScale != 0)
            TutorialComplete();
    }
    private void OnEnable() {
        EventBus.PauseInput += EventUpdate;
        EventBus.MovingTutorial += MovingTutorialComplete;
        EventBus.InventoryTutorial += InventoryTutorialComplete;
        EventBus.FeedTutorial += FeedTutorialComplete;
        EventBus.TransformationTutorial += TransformationTutorialComplete;
        EventBus.SellTutorial += SellTutorialComplete;
        EventBus.EatTutorial += EatTutorialComplete;
        EventBus.BuildTutorial += BuildTutorialComplete;
    }
    private void OnDisable()
    {
        EventBus.PauseInput -= EventUpdate;
        EventBus.MovingTutorial -= MovingTutorialComplete;
        EventBus.InventoryTutorial -= InventoryTutorialComplete;
        EventBus.FeedTutorial -= FeedTutorialComplete;
        EventBus.TransformationTutorial -= TransformationTutorialComplete;
        EventBus.SellTutorial -= SellTutorialComplete;
        EventBus.EatTutorial -= EatTutorialComplete;
        EventBus.BuildTutorial -= BuildTutorialComplete;
    }
    private bool IsPreviousTipsActive(int number)
    {
        for (int i = 0; i <= number; i++)
            if (_tips[i].activeSelf)
                return true;
        return false;
    }
    private void MovingTutorialComplete()
    {
        StartCoroutine(TipSwitchWithDelay(_tips[0], _tips[1]));
        EventBus.MovingTutorial -= MovingTutorialComplete;
    }
    private void InventoryTutorialComplete()
    {
        if (!IsPreviousTipsActive(0))
        {
            StartCoroutine(TipSwitchWithDelay(_tips[1], _tips[2]));
            EventBus.InventoryTutorial -= InventoryTutorialComplete;
        }
    }
    private void FeedTutorialComplete()
    {
        if (!IsPreviousTipsActive(1))
        {
            StartCoroutine(TipSwitchWithDelay(_tips[2], _tips[3]));
            EventBus.FeedTutorial -= FeedTutorialComplete;
        }
    }
    private void TransformationTutorialComplete()
    {
        if (!IsPreviousTipsActive(2))
        {
            StartCoroutine(TipSwitchWithDelay(_tips[3], _tips[4]));
            EventBus.TransformationTutorial -= TransformationTutorialComplete;
        }
    }
    private void SellTutorialComplete()
    {
        if (!IsPreviousTipsActive(3))
        {
            StartCoroutine(TipSwitchWithDelay(_tips[4], _tips[5]));
            EventBus.SellTutorial -= SellTutorialComplete;
        }
    }
    private void EatTutorialComplete()
    {
        if (!IsPreviousTipsActive(4))
        {
            StartCoroutine(TipSwitchWithDelay(_tips[5], _tips[6]));
            EventBus.EatTutorial -= EatTutorialComplete;
        }
    }
    private void BuildTutorialComplete()
    {
        if (!IsPreviousTipsActive(5))
        {
            StartCoroutine(TipSwitchWithDelay(_tutorialPanel, null));
            EventBus.BuildTutorial -= BuildTutorialComplete;
            TutorialComplete();
        }
    }
    private IEnumerator TipSwitchWithDelay(GameObject previousTip, GameObject nextTip)
    {
        yield return new WaitForSeconds(Constants.DEACTIVATE_PANEL_DELAY);
        previousTip.SetActive(false);
        if (nextTip != null)
            nextTip.SetActive(true);
    }
    public void TutorialComplete()
    {
        PlayerPrefs.SetInt("TutorialComplete", 1);
        PlayerPrefs.Save();
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
    public void NoSkip()
    {
        _askToTutorPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
}