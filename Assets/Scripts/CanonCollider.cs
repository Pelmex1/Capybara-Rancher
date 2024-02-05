using UnityEngine;

public class CanonCollider : MonoBehaviour
{
    [SerializeField] private InventoryPlayer inventoryPlayer;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("movebleObject")){
            if(inventoryPlayer.AddItemInInventory(other.gameObject.GetComponent<MovebleObject>().data)){
                Destroy(other.gameObject);
            }
        };
    }
}
