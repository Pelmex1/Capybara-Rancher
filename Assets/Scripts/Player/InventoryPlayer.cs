using CapybaraRancher.CustomStructures;
using System.Collections;
using CustomEventBus;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPlayer : MonoBehaviour, IInventory
{
    [SerializeField] private BoxCollider canonEnter;

    private int _index = 0;
    private int _localIndex;
    private const float SPEED = 10f;
    private Data _nullChestCell;
    public Data[] Inventory { get; set; } = new Data[5];
    private void Awake() {
        EventBus.AddItemInInventory = AddItemInInventory;
        EventBus.AddImageInInventory(this);
    }
    public bool AddItemInInventory(InventoryItem inventoryItem)
    {
        if (Inventory[_index].InventoryItem == null ||
            (Inventory[_index].InventoryItem == inventoryItem && Inventory[_index].Count < 20))
        {
            Inventory[_index].InventoryItem ??= inventoryItem;
            Inventory[_index]++;
            EventBus.SetCellsData(inventoryItem, inventoryItem.Image.sprite,(int)Inventory[_index], _index);
            return true;
        }
        
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i].InventoryItem == inventoryItem && Inventory[i].Count < 20)
            {
                Inventory[i].InventoryItem = inventoryItem;
                Inventory[i]++;
                EventBus.SetCellsData(inventoryItem, inventoryItem.Image.sprite,Inventory[i].Count, i);
                //playerAudioController.GunAddPlay();
                return true;
            } else if(Inventory[i].InventoryItem == null && _nullChestCell.Equals(null)){
                _nullChestCell = Inventory[i];
                _localIndex = i;
            }
        }
        if(!_nullChestCell.Equals(null)){
            _nullChestCell.InventoryItem = inventoryItem;
            _nullChestCell++;
            Inventory[_localIndex] = _nullChestCell;
            EventBus.SetCellsData(inventoryItem, inventoryItem.Image.sprite,Inventory[_localIndex].Count, _localIndex);
            _nullChestCell = new Data();
            return true;
        }
        return false;
    }

    public void RemoveItem(Vector3 spawnPos, Vector3 pos)
    {
        /*if (inventory[index].inventoryItem == null)
        {
            canon.Portal2.SetActive(false);
            return;
        }*/
        Inventory[_index]--;
        StartCoroutine(Recherge());
        GameObject localObject = Instantiate(Inventory[_index].InventoryItem.Prefab, spawnPos, Quaternion.identity);
        localObject.GetComponent<Rigidbody>().AddForce(pos, ForceMode.Impulse);
        if (Inventory[_index].Count == 0)
        {
            Inventory[_index].InventoryItem = null;
        }
    }
    private void Update()
    {
        float ScrollDelta = Input.mouseScrollDelta.y;
        if (ScrollDelta < 0 && _index < 5 && Time.timeScale == 1f)
            ChangeIndex(-1);
        else if (ScrollDelta > 0 && _index >= 0 && Time.timeScale == 1f)
            ChangeIndex(1);
        _index = IsButton();
        if (Input.GetMouseButtonDown(1))
        {
            RemoveItem(canonEnter.transform.position, -canonEnter.transform.forward * SPEED);
        }
    }
    private int IsButton() => Input.inputString switch {
        "1" => 0,
        "2" => 1,
        "3" => 2,
        "4" => 3,
        "5" => 4,
        _ => _index
    };
    private void ChangeIndex(int delta)
    {
        _index += delta;
        _index = Mathf.Clamp(_index, 0, 4);
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
