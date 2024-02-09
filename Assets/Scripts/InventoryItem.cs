using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Capybara-Rancher/InventoryItem", order = 0)]
public class InventoryItem : ScriptableObject {
    public Image image;
    public GameObject prefab;
    public string Name;
    public Color color;

    public int indexForSpawnFarm;
    public float timeGeneration;
    public bool isGenerable;
}
