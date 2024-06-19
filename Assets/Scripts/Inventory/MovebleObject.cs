using System.Collections;
using CapybaraRancher.Enums;
using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;
using UnityEngine;
using UnityEngine.AI;

public class MovebleObject : MonoBehaviour, IMovebleObject
{
    [SerializeField] private InventoryItem inventoryItem;
    private const string CANON_TAG = "CanonEnter";
    private NavMeshAgent _navMeshAgent;
    private bool _looted = false;
    private bool _isDisabled = false;

    public InventoryItem Data { get => inventoryItem; set => inventoryItem = value; }
    public GameObject Localgameobject { get => gameObject; set { return; } }
    public bool IsMoved { get; set; } = false;
    private void Start()
    {
        TryGetComponent(out _navMeshAgent);
    }
    private void Update()
    {
        if (Time.timeScale == 1f)
        {
            if (_navMeshAgent == null) return;
            _ = IsMoved ? (_navMeshAgent.enabled = false) : (_navMeshAgent.enabled = true);
        }
    }
    private void Pull(){
        if (Time.timeScale > 0)
        {
            if (EventBus.CheckList(gameObject)) IsMoved = true;
        }
        else IsMoved = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CANON_TAG) && !_looted && !_isDisabled)
        {
            if (EventBus.AddItemInInventory(Data))
            {
                _looted = true;
                EventBus.RemoveFromList(gameObject);
                ItemActivator.ActivatorItemsRemove(gameObject);
                gameObject.SetActive(false);
            }
        }
    }
    protected void OnDisable()
    {
        EventBus.PullInput -= Pull;
        _looted = false;
        EventBus.AddInPool(gameObject, Data.TypeGameObject);
    }
    private void OnEnable()
    {
        EventBus.PullInput += Pull;
        _isDisabled = true;
        StartCoroutine(Disabled());
    }
    private IEnumerator Disabled()
    {
        yield return new WaitForSecondsRealtime(2);
        _isDisabled = false;
    }
}

