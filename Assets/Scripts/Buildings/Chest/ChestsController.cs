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
        EventBus.FoundPos = FoundPos;
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
    private (int localIndex2, int localMindex, int newIndex, int newMIndex) FoundPos(Vector3 positionNow, Image image)
    {
        (int,int) localIndex = (-1,-1);
        for (int i = 0; i < ChestCells.Length; i++){
            if(i < InventoryCells.Length){
                if(image == InventoryCells[i]){
                    localIndex = (i,0);
                }
            }
            if(ChestCells[i] == image){
                localIndex = (i,1);
            }
        }
        float pos = 0;
        (int, int) index = (0,0);
        for (int i = 0; i < ChestCells.Length; i++)
        {
            if(i < InventoryCells.Length){
                float tmp1 = Math.Abs(Vector3.Distance(InventoryCells[i].gameObject.transform.position, positionNow));
                if (tmp1 > pos)
                {
                    pos = tmp1;
                    index = (i,0);
                }
            }
            float tmp = Math.Abs(Vector3.Distance(ChestCells[i].gameObject.transform.position, positionNow));
            if (tmp > pos)
            {
                pos = tmp;
                index = (i,1);
            }
        }
        return (localIndex.Item1,localIndex.Item2,index.Item1,index.Item2);
    }
    public void Exit()
    {
        Cursor.lockState = CursorLockMode.Locked;
        UIChestPanel.SetActive(false);
    }
}
