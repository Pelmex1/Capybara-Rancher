using CustomEventBus;
using UnityEngine;
using UnityEngine.AI;

public class MovebleObject : MonoBehaviour, IMovebleObject
{
    private const string CANON_TAG = "CanonEnter";
    private NavMeshAgent _navMeshAgent;
    private IMobsSpawner _mobsSpawner;

    public InventoryItem Data { get; set; }
    public GameObject Localgameobject {get; set;}
    public bool IsMoved {get; set;} = false;
    private void Awake() {
        SetLocalObject();
    }
    private void Start() {
        TryGetComponent(out _navMeshAgent);
        transform.parent?.TryGetComponent(out _mobsSpawner);
    }
    private void Update() 
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
        if (other.CompareTag(CANON_TAG))
        {
            if (EventBus.AddItemInInventory(Data))
            {
                EventBus.RemoveFromList(gameObject);
                if (_mobsSpawner == null) {
                    Destroy(gameObject); 
                }
            }
        };
    }
    public void SetLocalObject(){
        Localgameobject = gameObject;
    }
}
