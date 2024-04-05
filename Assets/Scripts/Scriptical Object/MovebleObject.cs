using CustomEventBus;
using UnityEngine;
using UnityEngine.AI;

public class MovebleObject : MonoBehaviour
{
    public InventoryItem data;
    public bool IsMoved {get; set;} = false;
    private NavMeshAgent navMeshAgent;
    private void Start() {
        TryGetComponent(out navMeshAgent);
    }
    private void Update() 
    {
        if(navMeshAgent == null) return;
        _ = IsMoved ? (navMeshAgent.enabled = false) : (navMeshAgent.enabled = true);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("CanonEnter")){
            if(EventBus.AddItemInInventory(data))
            {   
                EventBus.RemoveFromList(this);
                Destroy(other.gameObject);
            }
        };
    }
}
