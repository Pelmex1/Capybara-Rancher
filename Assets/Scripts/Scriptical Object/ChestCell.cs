using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChestCell : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Image image;   
    public InventoryItem inventoryItem;
    public int count;
    public delegate void AddDragEvent(Transform transform);
    public static event AddDragEvent SetCanvasParent;
    public delegate ChestCell AddCell(Vector3 position, ChestCell chestCell);
    public static event AddCell FoundDistance;
    private Vector3 pos;
    private Transform parentTransform;
    private ChestCell newChestCell;
    private void Start() {
        pos = transform.position;
        parentTransform = transform.parent;
    }
    public void OnDrag(PointerEventData pointerEventData){
        transform.position = pointerEventData.pointerCurrentRaycast.screenPosition;
    }
    public void OnBeginDrag(PointerEventData pointerEventData){
        SetCanvasParent.Invoke(transform);
    }
    public void OnEndDrag(PointerEventData pointerEventData){
        newChestCell = FoundDistance.Invoke(transform.position, newChestCell);
        if(newChestCell.inventoryItem == null){
            SetCellsData(ref newChestCell, inventoryItem, count);
            inventoryItem = null;
            count = 0;
            transform.position = pos;
            transform.SetParent(parentTransform);
        } else {
            InventoryItem inventoryItemTMP = newChestCell.inventoryItem;
            int countTMP = newChestCell.count;
            SetCellsData(ref newChestCell, inventoryItem, count);
            inventoryItem = inventoryItemTMP;
            count = countTMP;            
            transform.position = pos;
            transform.SetParent(parentTransform);
        }
    }
    private void SetCellsData(ref ChestCell chestCell, InventoryItem inventoryItem, int count){
        chestCell.inventoryItem = inventoryItem;
        chestCell.count = count;
    }
}
