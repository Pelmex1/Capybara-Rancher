using System.Collections;
using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;
using UnityEngine;
using UnityEngine.AI;

public class MovebleObject : MonoBehaviour, IMovebleObject
{
    [SerializeField] private InventoryItem inventoryItem;
    private const string CANON_TAG = "CanonEnter";
    private NavMeshAgent _navMeshAgent;
    private IObjectSpawner _objectSpawner;
    private bool _looted = false;
    private bool _isDisabled = false;

    public InventoryItem Data { get => inventoryItem; set => inventoryItem = value; }
    public GameObject Localgameobject { get => gameObject; set { return; } }
    public bool IsMoved { get; set; } = false;
    private void Start()
    {
        TryGetComponent(out _navMeshAgent);
        transform.parent?.TryGetComponent(out _objectSpawner);
    }
    protected virtual void Update()
    {
        if (Input.GetMouseButton(0) && Time.timeScale > 0)
        {
            if (EventBus.CheckList(gameObject)) IsMoved = true;
        }
        else IsMoved = false;

        if (Input.GetKeyDown(KeyCode.E) && Time.timeScale == 1f)
        {
            if (_navMeshAgent == null) return;
            _ = IsMoved ? (_navMeshAgent.enabled = false) : (_navMeshAgent.enabled = true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CANON_TAG) && !_looted && !_isDisabled)
        {
            _looted = true;
            if (EventBus.AddItemInInventory(Data))
            { 
                EventBus.AddInPool(gameObject, Data.TypeGameObject);
                EventBus.RemoveFromList(gameObject);
                ItemActivator.ActivatorItemsRemove(gameObject);
                _objectSpawner?.ReturnToPool(gameObject);
                gameObject.SetActive(false);
            }
        }
    }
    private void OnDisable()
    {
        _looted = false;
    }
    private void OnEnable()
    {
        _isDisabled = true;
        StartCoroutine(Disabled());
    }
    private IEnumerator Disabled()
    {
        yield return new WaitForSecondsRealtime(2);
        _isDisabled = false;
    }
}

