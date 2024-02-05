using System.Collections.Generic;
using UnityEngine;

public class InventoryPlayer : MonoBehaviour
{
    [SerializeField] private Transform canonEnter;
    //public Dictionary<InventoryItem,int>[] inventory = new Dictionary<InventoryItem,int>[5];
    public InventoryItem[] inventory = new InventoryItem[5];
    private int index = 0;

    public bool AddItemInInventory(InventoryItem inventoryItem){
        for(int i = 0; i < inventory.Length; i++){
            if(inventory[i] == null){
                inventory[i] = inventoryItem;
                return true;
            } else continue;
        }
        return false;
    }
    public void RemoveItem(Vector3 pos){
        if(inventory[index] == null) 
        {
            return;
        }
        else
        {
            Instantiate(inventory[index].prefab,pos,Quaternion.identity).GetComponent<Rigidbody>().AddForce(pos, ForceMode.Impulse);
            inventory[index] = null;
        }
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            index = 0;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            index = 1;
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            index = 2;
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)){
            index = 3;
        }
        if(Input.GetMouseButtonDown(1)){
            RemoveItem(canonEnter.position);
        }
    }
}
