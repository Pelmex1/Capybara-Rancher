using CapybaraRancher.CustomStructures;
using System.Collections;
using CustomEventBus;
using UnityEngine;

public class InventoryPlayer : MonoBehaviour
{
    [SerializeField] private BoxCollider canonEnter;

    private int index = 0;
    private int _localIndex;
    private const float SPEED = 10f;
    private CellData.Data _nullChestCell;
    public CellData.Data[] inventory = new CellData.Data[5];
    public bool WasChange = false;
    private void Awake() {
        EventBus.AddItemInInventory = AddItemInInventory;
    }
    private void Start() {
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i].Image = EventBus.GetImage(i);
        }
    }
    public bool AddItemInInventory(InventoryItem inventoryItem)
    {
        WasChange = true;

        if (inventory[index].InventoryItem == null ||
            (inventory[index].InventoryItem == inventoryItem && inventory[index].Count < 20))
        {
            inventory[index].InventoryItem ??= inventoryItem;
            inventory[index]++;
            CellData.Data tmp = inventory[index];
            EventBus.SetCellsData(tmp.InventoryItem, tmp.InventoryItem?.Image.sprite, tmp.Count, index);
            return true;
        }
        
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].InventoryItem == inventoryItem && inventory[index].Count < 20)
            {
                inventory[i].InventoryItem = inventoryItem;
                inventory[i]++;
                EventBus.SetCellsData(inventory[i].InventoryItem, inventory[i].InventoryItem?.Image.sprite, inventory[i].Count, index);
                //playerAudioController.GunAddPlay();
                return true;
            } else if(inventory[i].InventoryItem == null && _nullChestCell == null){
                _nullChestCell = inventory[i];
                _localIndex = i;
            }
        }
        if(_nullChestCell != null){
            _nullChestCell.InventoryItem = inventoryItem;
            _nullChestCell++;
            inventory[_localIndex] = _nullChestCell;
            EventBus.SetCellsData(inventory[_localIndex].InventoryItem, inventory[_localIndex].InventoryItem?.Image.sprite, inventory[_localIndex].Count, index);
            _nullChestCell = new CellData.Data();
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
        inventory[index].Count--;
        StartCoroutine(Recherge());
        GameObject localObject = Instantiate(inventory[index].InventoryItem.Prefab, spawnPos, Quaternion.identity);
        localObject.GetComponent<Rigidbody>().AddForce(pos, ForceMode.Impulse);
        if (inventory[index].Count == 0)
        {
            inventory[index].InventoryItem = null;
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
            inventory[lastindex].Image.color = Color.white;
            inventory[index].Image.color = Color.grey;
        }
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
