using CapybaraRancher.CustomStructures;
using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;
using CapybaraRancher.JsonSave;
using UnityEngine;

public class ContainerInventory : MonoBehaviour
{
    private bool _isNearChest = false;
    private readonly Data[] _chestCell = new Data[12];
    private IInventoryPlayer _inventoryPlayer;
    private void Start() {
        for(int i = 0; i < _chestCell.Length; i++){
            _chestCell[i] = JSONSerializer.Load<Data>($"{transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.name}_{i}") ?? new();
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
            int localIndex = counter.localIndex;
            int localMindex = counter.localMindex;
            int newIndex = counter.newIndex;
            int newMIndex = counter.newMIndex;
            Data[,] array = new Data[2,_chestCell.Length];
            for(int i = 0; i < _chestCell.Length; i++)
            {
                if(i < _inventoryPlayer.Inventory.Length){
                    array[0,i] = _inventoryPlayer.Inventory[i];
                }
                array[1,i] = _chestCell[i];
            }
            if(newMIndex == 1)
            {
                _chestCell[newIndex] = array[localMindex,localIndex];
            } else {
                _inventoryPlayer.Inventory[newIndex] = array[localMindex,localIndex];
            }
            if(localMindex == 1)
            {
                _chestCell[localIndex] = array[newMIndex,newIndex];
            } else 
            {
                _inventoryPlayer.Inventory[localIndex] = array[newMIndex,newIndex];
            }
            EventBus.OnRepaint(_inventoryPlayer.Inventory);
            EventBus.UpdateChestUI(_chestCell,_inventoryPlayer.Inventory);
        }
    }
    private void OnEnable() {
        EventBus.ChangeArray += ChangeArray;
    }
    private void OnDisable() {
        EventBus.ChangeArray -= ChangeArray;
    }
    private void OnApplicationQuit() {
        for (int i = 0; i < _chestCell.Length; i++)
        {
            JSONSerializer.Save($"{transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.name}_{i}",_chestCell[i]);
        }
    }
}
