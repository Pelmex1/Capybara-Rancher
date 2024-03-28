using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class ChestsController : MonoBehaviour
{
    [SerializeField] private GameObject HelpUi;
    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private GameObject ChestPanel;
    [SerializeField] private Transform CellsCanvas;
    [SerializeField] private Image defaultSprite;
    public ChestCell[] chestsUI = new ChestCell[20];
    public ChestCell[] inventoryUI = new ChestCell[5];


    private void Start()
    {
        inventoryUI = InventoryPanel.GetComponentsInChildren<ChestCell>();
        chestsUI = ChestPanel.GetComponentsInChildren<ChestCell>();
    }
    private void CellUpdate(ref ChestCell cell, ChestCell data){
        Debug.Log(cell);
        Debug.Log(cell.inventoryItem);
        cell.inventoryItem = data.inventoryItem;
        Debug.Log(cell);
        cell.count = data.count != 0 ? data.count : 0;
        Debug.Log(cell);
        cell.image = data.image != null ? data.image : defaultSprite;
        Debug.Log(cell);
    }
    private void UpdateChestCells(ChestCell[] chestCells)
    {
        for (int i = 0; i < chestCells.Length; i++)
        {
            CellUpdate(ref chestsUI[i],chestCells[i]);
        }
    }
    private void UpdateInventoryCells(ChestCell[] inventoryCells)
    {
        for (int i = 0; i < inventoryCells.Length; i++)
        {
            CellUpdate(ref inventoryUI[i], inventoryCells[i]);
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
                CellUpdate(ref chestCell, inventoryUI[i]);
            }
        }
        for (int j = 0; j < inventoryUI.Length; j++)
        {
            float tmp = Math.Abs(Vector3.Distance(inventoryUI[j].gameObject.transform.position, positionNow));
            if (tmp > pos)
            {
                pos = tmp;
                CellUpdate(ref chestCell, inventoryUI[j]);
            }
        }
        return chestCell;
    }
    private void SetParentObject(Transform transformGameObject)
    {
        transformGameObject.SetParent(CellsCanvas);
    }
    private void EnableHelpUI(bool isEnable){
        HelpUi.SetActive(isEnable);
    }
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
        ContainerInventory.EnableHelpUI +=EnableHelpUI;
        ContainerInventory.EnableBaseUI += EnableUI;
    }
    private void OnDisable()
    {
        ChestCell.SetCanvasParent -= SetParentObject;
        ChestCell.FoundDistance -= FoundPos;
        ContainerInventory.UpdateChestUI -= UpdateChestCells;
        ContainerInventory.UpdateInventoryUI -= UpdateInventoryCells;
        ContainerInventory.EnableHelpUI -= EnableHelpUI;
        ContainerInventory.EnableBaseUI -= EnableUI;
    }
}
