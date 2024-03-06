using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerInventory : MonoBehaviour
{
    private InventoryItem[] inventory = new InventoryItem[20];
    private int[] inventoryCount = new int[20];
    private bool AddItem(InventoryItem inventoryItem){
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == inventoryItem && inventoryCount[i] < 20)
            {
                inventoryCount[i]++;
                return true;
            }
        }
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = inventoryItem;
                inventoryCount[i]++;
                return true;
            }
            else continue;
        }
        return false;
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("movebleobject")){
            if(AddItem(other.gameObject.GetComponent<MovebleObject>().data)){
                Destroy(other.gameObject);
            };
        };
    }
}
