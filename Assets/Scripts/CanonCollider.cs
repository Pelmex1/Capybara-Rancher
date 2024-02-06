using UnityEngine;

public class CanonCollider : MonoBehaviour
{
    private InventoryPlayer inventoryPlayer;
    private void Start() {
        inventoryPlayer = GetComponentInParent<InventoryPlayer>();
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("movebleObject")){
            if(inventoryPlayer.AddItemInInventory(other.gameObject.GetComponent<MovebleObject>().data)){
                Canon.ItemCollection -= other.gameObject.GetComponent<MovebleObject>().OnTrigeredd;
                Destroy(other.gameObject);
            }
        };
    }
}
