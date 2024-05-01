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

    public InventoryItem Data { get => inventoryItem; set => inventoryItem = value; }
    public GameObject Localgameobject {get => gameObject; set{return;}}
    public bool IsMoved {get; set;} = false;
    private void Start()
    {
        TryGetComponent(out _navMeshAgent);
        transform.parent?.TryGetComponent(out _objectSpawner);
    }
    protected virtual void Update() 
    {
        if(Input.GetMouseButton(0) && Time.timeScale > 0){
            if(EventBus.CheckList(gameObject)) IsMoved = true;
        } else IsMoved = false;
        
        if(Input.GetKeyDown(KeyCode.E) && Time.timeScale == 1f){
            if(_navMeshAgent == null) return;
            _ = IsMoved ? (_navMeshAgent.enabled = false) : (_navMeshAgent.enabled = true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CANON_TAG) && !_looted)
        {
            _looted = true;
            if (EventBus.AddItemInInventory(Data))
            {
                EventBus.RemoveFromList(gameObject);
                if (_objectSpawner == null)
                    gameObject.SetActive(false);
                    EventBus.AddInPool(gameObject,Data.TypeGameObject);
                } else {
                    _objectSpawner.ReturnToPool(gameObject);
                }
            }
        }
    private void OnDisable() {
        _looted = false;
    }
}

