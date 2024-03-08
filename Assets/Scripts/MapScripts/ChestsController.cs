using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestsController : MonoBehaviour
{
    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private GameObject ChestPanel;
    private ChestCell[] chestsUI;
    private ChestCell[] inventoryUI;


    private void Start() {
        inventoryUI = InventoryPanel.GetComponentsInChildren<ChestCell>();
        chestsUI = ChestPanel.GetComponentsInChildren<ChestCell>();
    }
    public void UpdateChestCells(ChestCell[] chestCells){
        for(int i = 0 ; i < chestCells.Length; i++){
            chestsUI[i] = chestCells[i];
        }
    }
        public void UpdateInventoryCells(ChestCell[] inventoryCells){
        for(int i = 0 ; i < inventoryCells.Length; i++)
        {
            inventoryUI[i] = inventoryCells[i];
        }
    }
    private void OnEnable() 
    {
        ContainerInventory.UpdateChestUI += UpdateChestCells;
        ContainerInventory.UpdateInventoryUI += UpdateInventoryCells;
    }
    private void OnDisable() {
        ContainerInventory.UpdateChestUI -= UpdateChestCells;
        ContainerInventory.UpdateInventoryUI -= UpdateInventoryCells;
    }
}
