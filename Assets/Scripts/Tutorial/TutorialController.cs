using UnityEngine;
using CapybaraRancher.EventBus;
using System.Collections;

public class TutorialController : MonoBehaviour
{
    private const float DEACTIVATE_PANEL_DELAY = 2.5f;
    private const float START_MONEY = 300f;

    [SerializeField] private GameObject _tutorialPanel;
    [SerializeField] private GameObject _askToTutorPanel;
    [SerializeField] private GameObject[] _tips;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("TutorialComplete", 0) == 0)
        {
            if (EventBus.GetMoney() < 300f)
                EventBus.AddMoney(START_MONEY);
            _tutorialPanel.SetActive(true);
            _askToTutorPanel.SetActive(true);
            _tips[0].SetActive(true);

            EventBus.MovingTutorial += MovingTutorialComplete;
            EventBus.InventoryTutorial += InventoryTutorialComplete;
            EventBus.FeedTutorial += FeedTutorialComplete;
            EventBus.TransformationTutorial += TransformationTutorialComplete;
            EventBus.SellTutorial += SellTutorialComplete;
            EventBus.EatTutorial += EatTutorialComplete;
            EventBus.BuildTutorial += BuildTutorialComplete;
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            TutorialComplete();
    }
    private void OnDisable()
    {
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
        yield return new WaitForSeconds(DEACTIVATE_PANEL_DELAY);
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