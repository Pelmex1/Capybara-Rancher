using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Capybara-Rancher/InventoryItem")]
public class InventoryItem : ScriptableObject {
    public string Name;
    public Image image;
    public GameObject prefab;
    public Color color;
}
