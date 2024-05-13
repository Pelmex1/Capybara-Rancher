using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChestCell : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector3 _pos;
    private Transform _parentTransform;
    
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
        EventBus.SetChestParent(transform);
    }
    public void OnEndDrag(PointerEventData pointerEventData)
    { 
        /*_newChestCell = FoundDistance.Invoke(transform.position, _newChestCell);
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
        }*/
        transform.position = _pos;
        transform.SetParent(_parentTransform);
    }
    
}
