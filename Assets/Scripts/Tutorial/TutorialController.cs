using UnityEngine;
using CapybaraRancher.EventBus;
using System.Collections;

public class TutorialController : MonoBehaviour
{
    private const float DEACTIVATE_PANEL_DELAY = 2.5f;
    private const float floatStrength = 0.05f;
    private const float floatSpeed = 5f;

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
            _movingTip.SetActive(true);
        startPos = transform.position;
    }
    private void OnEnable()
    {
        EventBus.MovingTutorial += MovingTutorialComplete;
        EventBus.InventoryTutorial += InventoryTutorialComplete;
        EventBus.FeedTutorial += FeedTutorialComplete;
        EventBus.TransformationTutorial += TransformationTutorialComplete;
        EventBus.SellTutorial += SellTutorialComplete;
        EventBus.EatTutorial += EatTutorialComplete;
        EventBus.BuildTutorial += BuildTutorialComplete;
    }
    private void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatStrength;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    private void MovingTutorialComplete() 
    {
        StartCoroutine(TipDeactivateWithDelay(_movingTip));
        _inventoryTip.SetActive(true);
    }
    private void InventoryTutorialComplete()
    {
        StartCoroutine(TipDeactivateWithDelay(_inventoryTip));
        _feedTip.SetActive(true);
    }
    private void FeedTutorialComplete()
    {
        StartCoroutine(TipDeactivateWithDelay(_feedTip));
        _transformationTip.SetActive(true);
    }
    private void TransformationTutorialComplete()
    {
        StartCoroutine(TipDeactivateWithDelay(_transformationTip));
        _sellTip.SetActive(true);
    }
    private void SellTutorialComplete()
    {
        StartCoroutine(TipDeactivateWithDelay(_sellTip));
        _eatTip.SetActive(true);
    }
    private void EatTutorialComplete()
    {
        StartCoroutine(TipDeactivateWithDelay(_eatTip));
        _buildTip.SetActive(true);
    }
    private void BuildTutorialComplete()
    {
        StartCoroutine(TipDeactivateWithDelay(_buildTip));
        TutorialComplete();
    }
    private IEnumerator TipDeactivateWithDelay(GameObject tip)
    {
        yield return new WaitForSeconds(DEACTIVATE_PANEL_DELAY);
        tip.SetActive(false);
    }
    private void TutorialComplete() => PlayerPrefs.SetInt("TutorialComplete", 1);
}
