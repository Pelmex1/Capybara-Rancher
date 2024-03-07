using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPlayer : MonoBehaviour
{
    [SerializeField] private BoxCollider canonEnter;
    private int index = 0;
    private readonly float speed = 10f;
    private Canon canon;

    public Cell[] inventory = new Cell[5];
    public bool WasChange {get; set;} = false;

    private void Start() {
        canon = GetComponentInChildren<Canon>();
    }
    public bool AddItemInInventory(InventoryItem inventoryItem)
    {
        WasChange = true;
        if(inventory[index] == null){
            inventory[index].inventoryItem = inventoryItem;
            inventory[index].count++;
            inventory[index].image = inventoryItem.image;
            return true;
        } else if(inventory[index] == inventoryItem){
            inventory[index].count++;
            return true;
        }
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == inventoryItem)
            {
                inventory[index].count++;
                return true;
            }
        }
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i].inventoryItem = inventoryItem;
                inventory[i].count++;
                return true;
            }
            else continue;
        }
        return false;
    }
    public void RemoveItem(Vector3 spawnPos,Vector3 pos)
    {
        WasChange = true;
        if (inventory[index].inventoryItem == null)
        {
            return;
        }
        inventory[index].count--;
        StartCoroutine(Recherge());
        Instantiate(inventory[index].inventoryItem.prefab, spawnPos, Quaternion.identity).GetComponent<Rigidbody>().AddForce(pos, ForceMode.Impulse);
        if(inventory[index].count == 0)
        {
            inventory[index].inventoryItem = null;
        }
    }
    private void Update()
    {
        int lastindex = index;
        if (Input.GetKey(KeyCode.Alpha1))
            index = 0;
        if (Input.GetKey(KeyCode.Alpha2))
            index = 1;
        if (Input.GetKey(KeyCode.Alpha3))
            index = 2;
        if (Input.GetKey(KeyCode.Alpha4))
            index = 3;
        if (Input.GetKey(KeyCode.Alpha5))
            index = 4;

        //KeyCode code = KeyCode.None;
        //index = code switch
        //{
        //    KeyCode.Alpha1 => 0,
        //    KeyCode.Alpha2 => 1,
        //    KeyCode.Alpha3 => 2,
        //    KeyCode.Alpha4 => 3,
        //    _ => index
        //};
        inventory[lastindex].image.color = Color.white;
        inventory[index].image.color = Color.grey;
        if (Input.GetMouseButtonDown(1))
        {
            RemoveItem(canonEnter.transform.position,-canonEnter.transform.forward * speed);
        }
    }
    private IEnumerator Recherge(){
        canon.isIenumeratorenabled = true;
        canonEnter.enabled = false;
        yield return new WaitForSecondsRealtime(2);
        canonEnter.enabled = true;
        canon.isIenumeratorenabled = false;
    }
}
