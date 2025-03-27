using CapybaraRancher.Consts;
using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;
using UnityEngine;
using UnityEngine.AI;

public class CapybaraMovebleObject : MovebleObject
{
    private NavMeshAgent _navMeshAgent;
    private IObjectSpawner _objectSpawner;

    public bool IsMoved { get; set; } = false;

    private void Awake()
    {
        TryGetComponent(out _navMeshAgent);
        transform.parent?.TryGetComponent(out _objectSpawner);
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
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.CANON_TAG) && !_looted && !_isDisabled)
        {
            if (EventBus.AddItemInInventory(Data))
            {
                _looted = true;
                EventBus.RemoveFromList.Invoke(gameObject);
                EventBus.ActivatorItemsRemove.Invoke(gameObject);
                gameObject.SetActive(false);
                _objectSpawner?.ReturnToPool(gameObject);
            }
        }
    }
    protected override void OnEnable() {
        EventBus.PullInput += Pull;
    }
    protected override void OnDisable() {
        EventBus.AddInPool(gameObject, Data.TypeGameObject);
        EventBus.PullInput -= Pull;
    }
}

