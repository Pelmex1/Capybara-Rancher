using UnityEngine;

[CreateAssetMenu(fileName = "SavingCellData", menuName = "Capybara-Rancher/SavingCellData", order = 0)]
public class SavingCellData : ScriptableObject 
{
    public InventoryItem InventoryItem;
    public int Count;
}
