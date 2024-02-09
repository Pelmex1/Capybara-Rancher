using UnityEngine;

public class CanonCollider : MonoBehaviour
{
    private InventoryPlayer inventoryPlayer;
    private Canon Somecanon;
    private void Start() {
        Somecanon = GetComponentInParent<Canon>();
        inventoryPlayer = GetComponentInParent<InventoryPlayer>();
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("movebleObject")){
            if(inventoryPlayer.AddItemInInventory(other.gameObject.GetComponent<MovebleObject>().data)){
                try{Somecanon.obdjectsInCollider.Remove(other.gameObject.transform);}
                finally{
                    Destroy(other.gameObject);
                }
            }
        };
    }
}
