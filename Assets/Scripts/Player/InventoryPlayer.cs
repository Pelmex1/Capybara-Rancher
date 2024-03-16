using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class InventoryPlayer : MonoBehaviour
{
    [SerializeField] private BoxCollider canonEnter;

    private int index = 0;
    private readonly float speed = 10f;
    private Canon canon;

    public ChestCell[] inventory = new ChestCell[5];
    public bool WasChange = false;

    private void Start()
    {
        canon = GetComponentInChildren<Canon>();
    }
    public bool AddItemInInventory(InventoryItem inventoryItem)
    {
        WasChange = true;
        if (inventory[index].inventoryItem == null)
        {
            inventory[index].inventoryItem = inventoryItem;
            inventory[index].count++;
            return true;
        }
        else if (inventory[index].inventoryItem == inventoryItem && inventory[index].count < 20)
        {
            inventory[index].count++;
            return true;
        }
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].inventoryItem == inventoryItem && inventory[index].count < 20)
            {
                inventory[index].count++;
                return true;
            }
        }
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].inventoryItem == null)
            {
                inventory[i].inventoryItem = inventoryItem;
                inventory[i].count++;
                return true;
            }
            else continue;
        }

        return false;
    }
    public void RemoveItem(Vector3 spawnPos, Vector3 pos)
    {

        WasChange = true;
        if (inventory[index].inventoryItem == null)
        {
            canon.Portal2.SetActive(false);
            return;
        }
        inventory[index].count--;
        StartCoroutine(Recherge());
        Instantiate(inventory[index].inventoryItem.prefab, spawnPos, Quaternion.identity).GetComponent<Rigidbody>().AddForce(pos, ForceMode.Impulse);
        if (inventory[index].count == 0)
        {
            inventory[index].inventoryItem = null;
        }
    }
    private void Update()
    {
        int lastindex = index;
        float ScrollDelta = Input.mouseScrollDelta.y;
        if (ScrollDelta < 0 && index < 5)
            ChangeIndex(-1);
        else if (ScrollDelta > 0 && index >= 0)
            ChangeIndex(1);
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

        if (inventory[lastindex] != null && inventory[index] != null)
        {
            inventory[lastindex].image.color = Color.white;
            inventory[index].image.color = Color.grey;
        }
        if (Input.GetMouseButtonDown(1))
        {
            canon.Portal2.SetActive(true);
            RemoveItem(canonEnter.transform.position, -canonEnter.transform.forward * speed);
        }
    }

    private void ChangeIndex(int delta)
    {
        index += delta;
        index = Mathf.Clamp(index, 0, 4);
    }
    private IEnumerator Recherge()
    {

        canon.IsIenumeratorenabled = true;
        canonEnter.enabled = false;
        yield return new WaitForSecondsRealtime(2);

        canonEnter.enabled = true;
        canon.IsIenumeratorenabled = false;
    }
}
