using System;
using CustomEventBus;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour, ICell
{
    private const int NEVER_USED_INT = 99;
    public InventoryItem InventoryItem { get; set; }
    public Image Image { get; set; }
    public int Count { get; set; } = 0;
    private int index;
    private void Start() {
        index = Convert.ToInt32(name); // Так як у нас всього 5 комірок то ми можемо зробити так ( по іншому я не знаю як передавати індекс)
    }
    private int GetInt(int index){
        if(index == this.index){
            return Count;
        } else return NEVER_USED_INT; // щоб розлічити число 
    }
    private InventoryItem GetItem(int index){
        if(index == this.index){
            return InventoryItem;
        } else return EventBus.GetDefaultItem();
    }
    private void SetData(InventoryItem inventoryItem, Sprite image, int count, int index){
        if(index == this.index)
        {
            InventoryItem = count > 0 ? InventoryItem : inventoryItem;
            Image.sprite = image ?? EventBus.GetDefaultSprite();
            Count = count;
        }

    }
    public static Cell operator ++(Cell cell)
    {
        return new Cell { InventoryItem = cell.InventoryItem, Image = cell.Image, Count = cell.Count++ };
    }
    public static Cell operator --(Cell cell)
    {
        return new Cell { InventoryItem = cell.InventoryItem, Image = cell.Image, Count = cell.Count-- };
    }
    public static explicit operator int(Cell counter)
    {
        return counter.Count;
    }
    private void OnEnable() {
        EventBus.SetCellsData += SetData;
        EventBus.GetInt += GetInt;
        EventBus.GetInventoryItem += GetItem;
    }
    private void OnDisable() {
        EventBus.SetCellsData -= SetData;
        EventBus.GetInt -= GetInt;
        EventBus.GetInventoryItem -= GetItem;
    }
}
