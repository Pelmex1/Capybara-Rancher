using System;
using CapybaraRancher.CustomStructures;
using CapybaraRancher.EventBus;
using UnityEngine;
using UnityEngine.UI;

public class ChestsController : MonoBehaviour
{
    [SerializeField] private GameObject UIChestPanel;
    [SerializeField] private GameObject HelpUi;
    [SerializeField] private Image[] InventoryCells;
    [SerializeField] private Image[] ChestCells;
    [SerializeField] private Sprite DefaultSprite;

    private void Awake() {
        EventBus.EnableHelpUi = (bool isEnable) => HelpUi.SetActive(isEnable);
        EventBus.EnableChestUi = (bool isEnable) => UIChestPanel.SetActive(isEnable);
        EventBus.SetChestParent = (Transform localTransform) => localTransform.SetParent(UIChestPanel.transform);
        EventBus.UpdateChestUI = UpdateCells;
    }
    /*private ChestCell CellUpdate(ChestCell cell, ChestCell data){
        cell.InventoryItem = data.InventoryItem;
        cell.Count = data.Count != 0 ? data.Count : 0;
        cell.Image.sprite = data.InventoryItem?.Image ?? defaultSprite;
        return cell;
    }*/
    private void UpdateCells(Data[] chestCells,Data[] inventoryCells){
        for (int i = 0; i < chestCells.Length; i++)
        {
            if(i < inventoryCells.Length)
            {
                InventoryCells[i].sprite = inventoryCells[i].InventoryItem?.Image ?? DefaultSprite;
            }
            ChestCells[i].sprite = chestCells[i].InventoryItem?.Image ?? DefaultSprite;
        }
    }
    private void FoundPos(Vector3 positionNow, Image chestCell)
    {
        float pos = 0;
        for (int i = 0; i < ChestCells.Length; i++)
        {
            float tmp = Math.Abs(Vector3.Distance(InventoryCells[i].gameObject.transform.position, positionNow));
            if(i < InventoryCells.Length){
                if (tmp > pos)
                {
                    pos = tmp;
                }
            }
            tmp = Math.Abs(Vector3.Distance(ChestCells[i].gameObject.transform.position, positionNow));
            if (tmp > pos)
            {
                pos = tmp;
            }
        }
    }
    public void Exit()
    {
        Cursor.lockState = CursorLockMode.Locked;
        UIChestPanel.SetActive(false);
    }
}
