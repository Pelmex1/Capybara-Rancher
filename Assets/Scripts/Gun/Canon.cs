using System.Collections.Generic;
using CapybaraRancher.Consts;
using CapybaraRancher.Enums;
using CapybaraRancher.EventBus;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private BoxCollider canonEnter;
    [SerializeField] private GameObject Portal2;
    private const float SPEED = 3f;
    private Collider _colliderCanon;
    private bool _oneFunc = true;

    public List<GameObject> obdjectsInCollider = new();
    public bool Ienumeratorenabled { get; set; } = false;
    private void Awake()
    {
        EventBus.RemoveFromList = gameObject => obdjectsInCollider.Remove(gameObject);
        EventBus.InumeratorIsEnabled = isEnable => Ienumeratorenabled = isEnable;
        EventBus.CheckList = check => obdjectsInCollider.Contains(check);
    }
    private void Start()
    {
        _colliderCanon = GetComponent<BoxCollider>();
    }
    private void EventUpdate()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            if (!Ienumeratorenabled)
            {
                Portal2.SetActive(true);
                canonEnter.enabled = true;
            }
            for (int i = 0; i < obdjectsInCollider.Count; i++)
            {
                if (obdjectsInCollider[i] != null)
                    obdjectsInCollider[i].transform.position = Vector3.SlerpUnclamped(obdjectsInCollider[i].transform.position, canonEnter.transform.position, SPEED * Time.deltaTime);
                _oneFunc = true;
            }
            _colliderCanon.enabled = true;
            EventBus.PlayerGunAttraction.Invoke(true);
        }
    }
    private void NonPull(){
        canonEnter.enabled = false;
        Portal2.SetActive(false);
        if (_oneFunc)
        {
            canonEnter.enabled = false;
            _colliderCanon.enabled = false;
            obdjectsInCollider.Clear();
            _oneFunc = false;
        }
        EventBus.PlayerGunAttraction.Invoke(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.MOVEBLE_OBJECT_TAG))
        {
            obdjectsInCollider.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.MOVEBLE_OBJECT_TAG))
        {
            obdjectsInCollider.Remove(other.gameObject);
        }
    }
    private void OnEnable() {
        EventBus.PullInput += EventUpdate;
        EventBus.NonPullInput += NonPull;
    }
    private void OnDisable() {
        EventBus.PullInput -= EventUpdate;
        EventBus.NonPullInput -= NonPull;
    }
}
