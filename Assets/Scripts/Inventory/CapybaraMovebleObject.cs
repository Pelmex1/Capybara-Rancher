using CapybaraRancher.EventBus;
using UnityEngine;
using UnityEngine.AI;

public class CapybaraMovebleObject : MovebleObject
{
    private NavMeshAgent _navMeshAgent;
    
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
    protected override void OnTriggerEnter(Collider other)
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
    private void OnEnable() {
        EventBus.PullInput += Pull;
    }
    private void OnDisable() {
        EventBus.PullInput -= Pull;
    }
}

