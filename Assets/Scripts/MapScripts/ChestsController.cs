using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestsController : MonoBehaviour
{
    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private GameObject ChestPanel;
    [SerializeField] private Transform CellsCanvas;
    private ChestCell[] chestsUI;
    private ChestCell[] inventoryUI;


    private void Start() {
        inventoryUI = InventoryPanel.GetComponentsInChildren<ChestCell>();
        chestsUI = ChestPanel.GetComponentsInChildren<ChestCell>();
    }
    private void UpdateChestCells(ChestCell[] chestCells){
        for(int i = 0 ; i < chestCells.Length; i++){
            chestsUI[i] = chestCells[i];
        }
    }
    private void UpdateInventoryCells(ChestCell[] inventoryCells){
        for(int i = 0 ; i < inventoryCells.Length; i++)
        {
            inventoryUI[i] = inventoryCells[i];
        }
    }
    private ChestCell FoundPos(Vector3 positionNow, ChestCell chestCell){
        float pos = 0;
        for(int i = 0; i < chestsUI.Length; i++){
            float tmp2 = Math.Abs(Vector3.Distance(chestsUI[i].gameObject.transform.position, positionNow));     
            if(tmp2 > pos){
                pos = tmp2;
                chestCell = inventoryUI[i];
            }
        }
        for(int j = 0; j < inventoryUI.Length; j++){
            float tmp = Math.Abs(Vector3.Distance(inventoryUI[j].gameObject.transform.position, positionNow));
            if(tmp > pos){
                pos = tmp;
                chestCell = inventoryUI[j];
            }
        }
        return chestCell;
    }
    private void SetParentObject(Transform transformGameObject){
        transformGameObject.SetParent(CellsCanvas);
    }
    private void OnEnable() 
    {
        ChestCell.SetCanvasParent += SetParentObject;
        ChestCell.FoundDistance += FoundPos;
        ContainerInventory.UpdateChestUI += UpdateChestCells;
        ContainerInventory.UpdateInventoryUI += UpdateInventoryCells;
    }
    private void OnDisable() {
        ChestCell.SetCanvasParent -= SetParentObject;
        ChestCell.FoundDistance -= FoundPos;
        ContainerInventory.UpdateChestUI -= UpdateChestCells;
        ContainerInventory.UpdateInventoryUI -= UpdateInventoryCells;
    }
}
