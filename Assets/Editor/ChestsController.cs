using System;
using CapybaraRancher.EventBus;
using UnityEngine;

public class ChestsController : MonoBehaviour
{
    [SerializeField] private GameObject HelpUi;
    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private GameObject ChestPanel;
    [SerializeField] private Transform CellsCanvas;
    [SerializeField] private Sprite defaultSprite;
    public ChestCell[] chestsUI = new ChestCell[12];
    public ChestCell[] inventoryUI = new ChestCell[5];
    private void Awake() {
        EventBus.EnableHelpUi = EnableHelpUI;
    }
    private ChestCell CellUpdate(ChestCell cell, ChestCell data){
        cell.InventoryItem = data.InventoryItem;
        cell.Count = data.Count != 0 ? data.Count : 0;
        cell.Image.sprite = data.InventoryItem?.Image ?? defaultSprite;
        return cell;
    }
    private void UpdateChestCells(ChestCell[] chestCells)
    {
        for (int i = 0; i < chestCells.Length; i++)
        {
            chestsUI[i] = CellUpdate(chestsUI[i],chestCells[i]);
        }
    }
    private void UpdateInventoryCells(ChestCell[] inventoryCells)
    {
        for (int i = 0; i < inventoryCells.Length; i++)
        {
            chestsUI[i] = CellUpdate(inventoryUI[i], inventoryCells[i]);
        }
    }
    private ChestCell FoundPos(Vector3 positionNow, ChestCell chestCell)
    {
        float pos = 0;
        for (int i = 0; i < chestsUI.Length; i++)
        {
            float tmp2 = Math.Abs(Vector3.Distance(chestsUI[i].gameObject.transform.position, positionNow));
            if (tmp2 > pos)
            {
                pos = tmp2;
                chestCell = CellUpdate(chestCell, inventoryUI[i]);
            }
        }
        for (int j = 0; j < inventoryUI.Length; j++)
        {
            float tmp = Math.Abs(Vector3.Distance(inventoryUI[j].gameObject.transform.position, positionNow));
            if (tmp > pos)
            {
                pos = tmp;
                chestCell = CellUpdate(chestCell, inventoryUI[j]);
            }
        }
        return chestCell;
    }
    private void SetParentObject(Transform transformGameObject)
    {
        transformGameObject.SetParent(CellsCanvas);
    }
    private void EnableHelpUI(bool isEnable) => HelpUi.SetActive(isEnable);
    private void EnableUI(bool isEnable){
        InventoryPanel.SetActive(isEnable);
        ChestPanel.SetActive(isEnable);
    }
    private void OnEnable()
    {
        ChestCell.SetCanvasParent += SetParentObject;
        ChestCell.FoundDistance += FoundPos;
        ContainerInventory.UpdateChestUI += UpdateChestCells;
        ContainerInventory.UpdateInventoryUI += UpdateInventoryCells;
        ContainerInventory.EnableBaseUI += EnableUI;
    }
    private void OnDisable()
    {
        ChestCell.SetCanvasParent -= SetParentObject;
        ChestCell.FoundDistance -= FoundPos;
        ContainerInventory.UpdateChestUI -= UpdateChestCells;
        ContainerInventory.UpdateInventoryUI -= UpdateInventoryCells;
        ContainerInventory.EnableBaseUI -= EnableUI;
    }
}
