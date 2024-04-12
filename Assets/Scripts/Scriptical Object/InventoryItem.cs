using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Capybara-Rancher/InventoryItem")]
public class InventoryItem : ScriptableObject {
    public string Name;
    public Sprite Image;
    public GameObject Prefab;
    public Color Color;
}
