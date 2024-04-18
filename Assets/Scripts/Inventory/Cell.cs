using System;
using CustomEventBus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour, ICell
{
    private const int NEVER_USED_INT = 99;
    public InventoryItem InventoryItem { get; set; }
    public Image Image { get; set; }
    public int Count { get; set; } = 0;
    public SaveCellData SaveCellData {get; set;}
    [SerializeField] private SaveCellData _saveCellData;
    private int index;
    private TMP_Text _countText;
    private void Start()
    {
        SaveCellData = _saveCellData;
        Image = GetComponentInChildren<Image>();
        _countText = GetComponentInChildren<TMP_Text>();
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
}
