using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChestCell : MonoBehaviour, ICell,IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Image Image {get; set;}
    public InventoryItem InventoryItem {get; set;}
    public int Count {get; set;}
    public delegate void AddDragEvent(Transform transform);
    public static event AddDragEvent SetCanvasParent;
    public delegate ChestCell AddCell(Vector3 position, ChestCell chestCell);
    public static event AddCell FoundDistance;
    private Vector3 _pos;
    private Transform _parentTransform;
    private ChestCell _newChestCell;
    private void Start()
    {
        _pos = transform.position;
        _parentTransform = transform.parent;
    }
    public void OnDrag(PointerEventData pointerEventData)
    {
        transform.position = pointerEventData.pointerCurrentRaycast.screenPosition;
    }
    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        SetCanvasParent.Invoke(transform);
    }
    public void OnEndDrag(PointerEventData pointerEventData)
    {
        _newChestCell = FoundDistance.Invoke(transform.position, _newChestCell);
        if (_newChestCell.InventoryItem == null)
        {
            SetCellsData(ref _newChestCell, InventoryItem, Count);
            InventoryItem = null;
            Count = 0;
            transform.position = _pos;
            transform.SetParent(_parentTransform);
        }
        else
        {
            InventoryItem inventoryItemTMP = _newChestCell.InventoryItem;
            int countTMP = _newChestCell.Count;
            SetCellsData(ref _newChestCell, InventoryItem, Count);
            InventoryItem = inventoryItemTMP;
            Count = countTMP;
            transform.position = _pos;
            transform.SetParent(_parentTransform);
        }
    }
    private void SetCellsData(ref ChestCell chestCell, InventoryItem inventoryItem, int count)
    {
        chestCell.InventoryItem = inventoryItem;
        chestCell.Count = count;
    }
}
