using UnityEngine;

public class CanonCollider : MonoBehaviour
{
    private InventoryPlayer inventoryPlayer;
    private Canon Somecanon;
    private void Start() {
        Somecanon = GetComponentInParent<Canon>();
        inventoryPlayer = GetComponentInParent<InventoryPlayer>();
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("movebleObject"))
        {
            MovebleObject movebleObject = other.gameObject.GetComponent<MovebleObject>();
            if(inventoryPlayer.AddItemInInventory(movebleObject.data))
            {   
                Somecanon.obdjectsInCollider.Remove(movebleObject);
                Destroy(other.gameObject);
            }
        };
    }
}
