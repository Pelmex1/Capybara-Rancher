using UnityEngine;

public class MovebleObject : MonoBehaviour
{
    public InventoryItem data;
    private readonly float speed = 0.1f;
    
    public void OnTrigeredd(Transform pos){
        transform.position = Vector3.SlerpUnclamped(transform.position, pos.position, speed * Time.deltaTime);
    }
    private void OnDestroy() {
        Canon.ItemCollection -= OnTrigeredd;
    }
}
