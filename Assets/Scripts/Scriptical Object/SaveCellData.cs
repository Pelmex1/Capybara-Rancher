using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SaveCellData", menuName = "Capybara-Rancher/SaveCellData")]
public class SaveCellData : ScriptableObject
{
    public InventoryItem InventoryItem;
    public int Count;
    public Image Image;
}
