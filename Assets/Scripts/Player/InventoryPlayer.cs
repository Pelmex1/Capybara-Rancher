using System.Collections;
using System.Collections.Generic;
using CustomEventBus;
using UnityEngine;
using UnityEngine.Rendering;

public class InventoryPlayer : MonoBehaviour
{
    [SerializeField] private BoxCollider canonEnter;

    private int index = 0;
    private readonly float speed = 10f;
    //private Canon canon;
    //private PlayerAudioController playerAudioController;
    private ChestCell nullChestCell;
    private Dictionary<InventoryItem, int> CellsData;
    public ChestCell[] inventory = new ChestCell[5];
    public bool WasChange = false;
    private void Awake() {
        EventBus.AddItemInInventory = AddItemInInventory;
    }
    public bool AddItemInInventory(InventoryItem inventoryItem)
    {
        WasChange = true;

        if (inventory[index].inventoryItem == null ||
            (inventory[index].inventoryItem == inventoryItem && inventory[index].count < 20))
        {
            inventory[index].inventoryItem ??= inventoryItem;
            inventory[index].count++;
            //playerAudioController.GunAddPlay();
            return true;
        }
        
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].inventoryItem == inventoryItem && inventory[index].count < 20)
            {
                inventory[i].inventoryItem = inventoryItem;
                inventory[i].count++;
                //playerAudioController.GunAddPlay();
                return true;
            } else if(inventory[i].inventoryItem == null && nullChestCell == null){
                nullChestCell = inventory[i];
            }
        }
        if(nullChestCell != null){
            nullChestCell.inventoryItem = inventoryItem;
            nullChestCell.count++;
            nullChestCell = null;
            //playerAudioController.GunAddPlay();
            return true;
        }
        return false;
    }

    public void RemoveItem(Vector3 spawnPos, Vector3 pos)
    {
        WasChange = true;
        /*if (inventory[index].inventoryItem == null)
        {
            canon.Portal2.SetActive(false);
            return;
        }*/
        inventory[index].count--;
        StartCoroutine(Recherge());
        Instantiate(inventory[index].inventoryItem.prefab, spawnPos, Quaternion.identity).GetComponent<Rigidbody>().AddForce(pos, ForceMode.Impulse);
        if (inventory[index].count == 0)
        {
            inventory[index].inventoryItem = null;
        }

        //playerAudioController.GunRemovePlay();
    }
    private void Update()
    {
        int lastindex = index;
        float ScrollDelta = Input.mouseScrollDelta.y;
        if (ScrollDelta < 0 && index < 5 && Time.timeScale == 1f)
            ChangeIndex(-1);
        else if (ScrollDelta > 0 && index >= 0 && Time.timeScale == 1f)
            ChangeIndex(1);
        index = IsButton();
        if (inventory[lastindex] != null && inventory[index] != null)
        {
            inventory[lastindex].image.color = Color.white;
            inventory[index].image.color = Color.grey;
        }
        if (Input.GetMouseButtonDown(1))
        {
            RemoveItem(canonEnter.transform.position, -canonEnter.transform.forward * speed);
        }
    }
    private int IsButton() => Input.inputString switch {
        "1" => 0,
        "2" => 1,
        "3" => 2,
        "4" => 3,
        "5" => 4,
        _ => index
    };
    private void ChangeIndex(int delta)
    {
        index += delta;
        index = Mathf.Clamp(index, 0, 4);
    }
    private IEnumerator Recherge()
    {
        EventBus.InumeratorIsEnabled(true);
        canonEnter.enabled = false;
        yield return new WaitForSecondsRealtime(3);
        canonEnter.enabled = true;
        EventBus.InumeratorIsEnabled(false);
    }
}
