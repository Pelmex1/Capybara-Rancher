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
    private readonly Data[] _chestCell = new Data[12];
    private IInventoryPlayer _inventoryPlayer;
    private EventBus _eventBus;
    private void Start() {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        IGetSaveName saveClass = new();
        _eventBus.Invoke(saveClass);
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
            _eventBus.Invoke<IEnableChestUI>(new(true));
            _eventBus.Invoke<IEnableHelpUI>(new(false));
            _eventBus.Invoke<IUpdateChestUI>(new(_chestCell,_inventoryPlayer.Inventory));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.PLAYER_TAG))
        { 
            _inventoryPlayer = other.GetComponent<IInventoryPlayer>();
            _isNearChest = true;
            _eventBus.Invoke<IEnableHelpUI>(new(true));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.PLAYER_TAG))
        {
            _isNearChest = false;
            _eventBus.Invoke<IEnableHelpUI>(new(false));
        }
    }
    private void ChangeArray(IChangeChestArray iChangeChestArrayClass)
    {
        if(_isNearChest)
        {
            int localIndex = iChangeChestArrayClass.indexer.OldInventoryIndex;
            int localMindex = iChangeChestArrayClass.indexer.NewInventoryIndex;
            int newIndex = iChangeChestArrayClass.indexer.OldChestIndex;
            int newMIndex = iChangeChestArrayClass.indexer.NewInventoryIndex;
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
            _eventBus.Invoke<IRepaintInventory>(new(_inventoryPlayer.Inventory));
            _eventBus.Invoke<IUpdateChestUI>(new(_chestCell,_inventoryPlayer.Inventory));
        }
    }
    private void OnEnable() {
        EventBus.TerminalUseInput += EventUpdate;
        _eventBus.Subscribe<IChangeChestArray>(ChangeArray);
        _eventBus.Subscribe<IGlobalSave>(Save);
    }
    private void OnDisable() {
        EventBus.TerminalUseInput -= EventUpdate;
        _eventBus.UnSubscribe<IChangeChestArray>(ChangeArray);
        _eventBus.UnSubscribe<IGlobalSave>(Save);
        Array.Clear(_chestCell,0,_chestCell.Length);      
    }
    private void OnApplicationQuit()
    {
        Save(new());
    }
    private void Save(IGlobalSave globalSave){
        IGetSaveName saveClass = new();
        _eventBus.Invoke(saveClass);
        for (int i = 0; i < _chestCell.Length; i++)
        { 
            if(_chestCell[i].InventoryItem != null){
                JSONSerializer.Save($"{saveClass.SaveString}_{transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.name}_{i}",_chestCell[i]);
            }
        }
    }
}
