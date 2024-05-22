using UnityEngine;
using CapybaraRancher.EventBus;
using System.Collections;
using CapybaraRancher.Interfaces;

public class TutorialController : MonoBehaviour
{
    private const float DEACTIVATE_PANEL_DELAY = 2.5f;
    private const float FLOAT_STRENGTH = 0.5f;
    private const float FLOAT_SPEED = 5f;
    private const float START_MONEY = 300f;

    [SerializeField] private GameObject _tutorialPanel;
    [SerializeField] private GameObject _movingTip;
    [SerializeField] private GameObject _inventoryTip;
    [SerializeField] private GameObject _feedTip;
    [SerializeField] private GameObject _transformationTip;
    [SerializeField] private GameObject _sellTip;
    [SerializeField] private GameObject _eatTip;
    [SerializeField] private GameObject _buildTip;

    private Vector3 startPos;

    private void Start()
    {
        if (PlayerPrefs.GetInt("TutorialComplete", 0) == 0)
        {
            EventBus.AddMoney(START_MONEY);
            _tutorialPanel.SetActive(true);
            _movingTip.SetActive(true);
            Subscriptions();
        }
        startPos = transform.position;
    }
    private void Subscriptions()
    {
        EventBus.MovingTutorial += MovingTutorialComplete;
        EventBus.InventoryTutorial += InventoryTutorialComplete;
        EventBus.FeedTutorial += FeedTutorialComplete;
        EventBus.TransformationTutorial += TransformationTutorialComplete;
        EventBus.SellTutorial += SellTutorialComplete;
        EventBus.EatTutorial += EatTutorialComplete;
        EventBus.BuildTutorial += BuildTutorialComplete;
    }
    private void UnSubscriptions()
    {
        EventBus.MovingTutorial -= MovingTutorialComplete;
        EventBus.InventoryTutorial -= InventoryTutorialComplete;
        EventBus.FeedTutorial -= FeedTutorialComplete;
        EventBus.TransformationTutorial -= TransformationTutorialComplete;
        EventBus.SellTutorial -= SellTutorialComplete;
        EventBus.EatTutorial -= EatTutorialComplete;
        EventBus.BuildTutorial -= BuildTutorialComplete;
    }
    private void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * FLOAT_SPEED) * FLOAT_STRENGTH;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    private void MovingTutorialComplete() 
    {
        StartCoroutine(TipSwitchWithDelay(_movingTip, _inventoryTip));
        EventBus.MovingTutorial -= MovingTutorialComplete;
    }
    private void InventoryTutorialComplete()
    {
        StartCoroutine(TipSwitchWithDelay(_inventoryTip, _feedTip));
        EventBus.InventoryTutorial -= InventoryTutorialComplete;
    }
    private void FeedTutorialComplete()
    {
        StartCoroutine(TipSwitchWithDelay(_feedTip, _transformationTip));
        EventBus.FeedTutorial -= FeedTutorialComplete;
    }
    private void TransformationTutorialComplete()
    {
        StartCoroutine(TipSwitchWithDelay(_transformationTip, _sellTip));
        EventBus.TransformationTutorial -= TransformationTutorialComplete;
    }
    private void SellTutorialComplete()
    {
        StartCoroutine(TipSwitchWithDelay(_sellTip, _eatTip));
        EventBus.SellTutorial -= SellTutorialComplete;
    }
    private void EatTutorialComplete()
    {
        StartCoroutine(TipSwitchWithDelay(_eatTip, _buildTip));
        EventBus.EatTutorial -= EatTutorialComplete;
    }
    private void BuildTutorialComplete()
    {
        StartCoroutine(TipSwitchWithDelay(_tutorialPanel, null));
        EventBus.BuildTutorial -= BuildTutorialComplete;
        TutorialComplete();
    }
    private IEnumerator TipSwitchWithDelay(GameObject previousTip, GameObject nextTip)
    {
        yield return new WaitForSeconds(DEACTIVATE_PANEL_DELAY);
        previousTip.SetActive(false);
        if (nextTip != null)
            nextTip.SetActive(true);
    }
    private void TutorialComplete()
    {
        PlayerPrefs.SetInt("TutorialComplete", 1);
        UnSubscriptions();
    }
}
