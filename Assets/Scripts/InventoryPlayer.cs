using UnityEngine;

public class InventoryPlayer : MonoBehaviour
{
    [SerializeField] private Transform canonEnter;
    private int index = 0;

    public InventoryItem[] inventory = new InventoryItem[5];
    public int[] inventoryCount = new int[5];
    public bool AddItemInInventory(InventoryItem inventoryItem){
        for(int i = 0; i < inventory.Length; i++){
            if(inventory[i] == inventoryItem){
                inventoryCount[i]++;
                return true;
            }
        }
        for(int i = 0; i < inventory.Length; i++){
            if(inventory[i] == null){
                inventory[i] = inventoryItem;
                inventoryCount[i]++;
                return true;
            } else continue;
        }
        return false;
    }
    public void RemoveItem(Vector3 pos){
        if(inventory[index] == null) 
        {
            return;
        } else if(inventoryCount[index] > 1){
            inventoryCount[index]--;
            Instantiate(inventory[index].prefab,pos,Quaternion.identity).GetComponent<Rigidbody>().AddForce(pos, ForceMode.Impulse);
        } else
        {
            inventoryCount[index]--;
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
            RemoveItem(canonEnter.position + canonEnter.forward);
        }
    }
}
