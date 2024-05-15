using CapybaraRancher.CustomStructures;
using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;
using UnityEngine;

public class ContainerInventory : MonoBehaviour
{
    private bool _isNearChest = false;
    private readonly Data[] _chestCell = new Data[11];
    private IInventoryPlayer _inventoryPlayer;
    private void Start() {
        for(int i = 0; i < _chestCell.Length; i++){
            _chestCell[i] = new();
        }
    }
    private void Update()
    {
        if (_isNearChest && Input.GetKeyDown(KeyCode.E))
        {
            Cursor.lockState = CursorLockMode.Confined;
            EventBus.EnableChestUi(true);
            EventBus.EnableHelpUi(false);
            EventBus.UpdateChestUI(_chestCell,_inventoryPlayer.Inventory);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            _inventoryPlayer = other.GetComponent<IInventoryPlayer>();
            _isNearChest = true;
            EventBus.EnableHelpUi(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isNearChest = false;
            EventBus.EnableHelpUi(false);
        }
    }
    private void ChangeArray((int localIndex, int localMindex, int newIndex, int newMIndex) counter)
    {
        if(_isNearChest)
        {
            Data[,] array = new Data[2,_chestCell.Length];
            for(int i = 0; i < _chestCell.Length; i++)
            {
                if(i < _inventoryPlayer.Inventory.Length){
                    array[0,i] = _inventoryPlayer.Inventory[i];
                }
                array[1,i] = _chestCell[i];
            }
            if(counter.newMIndex == 1)
            {
                _chestCell[counter.newIndex] = array[counter.localMindex,counter.localIndex];
            } else {
                _inventoryPlayer.Inventory[counter.newIndex] = array[counter.localMindex,counter.localIndex];
            }
            if(counter.localMindex == 1)
            {
                _chestCell[counter.localIndex] = array[counter.newMIndex,counter.newIndex];
            } else 
            {
                _inventoryPlayer.Inventory[counter.localIndex] = array[counter.newMIndex,counter.newIndex];
            }
            EventBus.UpdateChestUI(_inventoryPlayer.Inventory, _chestCell);
            EventBus.OnRepaint(_inventoryPlayer.Inventory);
        }
    }
    private void OnEnable() {
        EventBus.ChangeArray += ChangeArray;
    }
    private void OnDisable() {
        EventBus.ChangeArray -= ChangeArray;
    }
}
