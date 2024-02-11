using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Capybara-Rancher/InventoryItem", order = 0)]
public class InventoryItem : ScriptableObject {
    public string Name;
    public Image image;
    public GameObject prefab;
    public Color color;

    // Fields for Foods
    public int indexForSpawnFarm;
    public float timeGeneration;
    public bool isGenerable;

    // Fields for Capybaras
    public GameObject point;

    // Fields for Crytals
    public int minPrice;
    public int maxPrice;
}
