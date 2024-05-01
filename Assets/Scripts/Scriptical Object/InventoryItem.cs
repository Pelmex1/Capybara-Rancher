using CapybaraRancher.Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Capybara-Rancher/InventoryItem")]
public class InventoryItem : ScriptableObject {
    public string Name;
    public Sprite Image;
    public GameObject Prefab;
    public Color Color;
    public TypeGameObject TypeGameObject;
}
