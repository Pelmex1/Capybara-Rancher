using System;
using UnityEngine;
using CustomEventBus;
using CapybaraRancher.Interfaces;
using CapybaraRancher.Save;
using CapybaraRancher.Abstraction.CustomStructures;
using CapybaraRancher.Abstraction.Signals.Chest;
using CapybaraRancher.Consts;
using CapybaraRancher.Abstraction.Signals;

public class ContainerInventory : MonoBehaviour
{
    private bool _isNearChest = false;
    private Data[] _chestCell = new Data[12];
    private IInventoryPlayer _inventoryPlayer;
    private EventBus eventBus;
    private void Start() {
        eventBus = ServiceLocator.Current.Get<EventBus>();
        IGetSaveName saveClass = new();
        eventBus.Invoke(saveClass);
        for(int i = 0; i < _chestCell.Length; i++){;
            _chestCell[i] = JSONSerializer.Load<Data>($"{saveClass.SaveString}_{transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.name}_{i}") ?? new();
            FileEditor.DeleteFile($"Save/{saveClass.SaveString}_{transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.name}_{i}.json");
        }
    }
    private void EventUpdate()
    {
        if (_isNearChest)
        {
            Cursor.lockState = CursorLockMode.Confined;
            eventBus.Invoke<IEnableChestUI>(new(true));
            eventBus.Invoke<IEnableHelpUI>(new(false));
            eventBus.Invoke<IUpdateChestUI>(new(_chestCell,_inventoryPlayer.Inventory));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.PLAYER_TAG))
        { 
            _inventoryPlayer = other.GetComponent<IInventoryPlayer>();
            _isNearChest = true;
            eventBus.Invoke<IEnableHelpUI>(new(true));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.PLAYER_TAG))
        {
            _isNearChest = false;
            eventBus.Invoke<IEnableHelpUI>(new(false));
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
            Data[,] cells = new Data[2,_chestCell.Length];
            for(int i = 0; i < _chestCell.Length; i++)
            {
                if(i < _inventoryPlayer.Inventory.Length){
                    cells[0,i] = _inventoryPlayer.Inventory[i];
                }
                cells[1,i] = _chestCell[i];
            }
            if(newMIndex == 1)
            {
                _chestCell[newIndex] = cells[localMindex,localIndex];
            } else {
                _inventoryPlayer.Inventory[newIndex] = cells[localMindex,localIndex];
            }
            if(localMindex == 1)
            {
                _chestCell[localIndex] = cells[newMIndex,newIndex];
            } else 
            {
                _inventoryPlayer.Inventory[localIndex] = cells[newMIndex,newIndex];
            }
            eventBus.Invoke<IRepaintInventory>(new(_inventoryPlayer.Inventory));
            eventBus.Invoke<IUpdateChestUI>(new(_chestCell,_inventoryPlayer.Inventory));
        }
    }
    private void OnEnable() {
        EventBus.TerminalUseInput += EventUpdate;
        EventBus.ChangeArray += ChangeArray;
        EventBus.GlobalSave += Save;
    }
    private void OnDisable() {
        EventBus.TerminalUseInput -= EventUpdate;
        EventBus.ChangeArray -= ChangeArray;
        EventBus.GlobalSave -= Save;
        Array.Clear(_chestCell,0,_chestCell.Length);      
    }
    private void OnApplicationQuit()
    {
        Save();
    }
    private void Save(){
        IGetSaveName saveClass = new();
        eventBus.Invoke(saveClass);
        for (int i = 0; i < _chestCell.Length; i++)
        { 
            if(_chestCell[i].InventoryItem != null){
                JSONSerializer.Save($"{saveClass.SaveString}_{transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.name}_{i}",_chestCell[i]);
            }
        }
    }
}
