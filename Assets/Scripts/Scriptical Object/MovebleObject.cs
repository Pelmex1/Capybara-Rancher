using CustomEventBus;
using UnityEngine;
using UnityEngine.AI;

public class MovebleObject : MonoBehaviour, IMovebleObject
{
    private NavMeshAgent _navMeshAgent;
    private IMobsSpawner _mobsSpawner;

    public InventoryItem Data { get => Data; set {return;} }
    public GameObject Localgameobject {get => Localgameobject; set {return;}}
    public bool IsMoved {get; set;} = false;
    private void Awake() {
        SetLocalObject();
    }
    private void Start() {
        TryGetComponent(out _navMeshAgent);
        transform.parent.TryGetComponent(out _mobsSpawner);
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
        if (other.CompareTag("CanonEnter"))
        {
            if (EventBus.AddItemInInventory(Data))
            {
                EventBus.RemoveFromList(gameObject);
                if (_mobsSpawner == null) { Destroy(gameObject); }
                else { _mobsSpawner.ReturnToPool(gameObject); }
            }
            _mobsSpawner.ReturnToPool(gameObject);
        };
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _mobsSpawner.ReturnToPool(gameObject);
        }
    }
    public void SetLocalObject(){
        Localgameobject = gameObject;
    }
}
