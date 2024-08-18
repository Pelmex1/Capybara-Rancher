using UnityEngine;

[CreateAssetMenu(fileName = "FarmObject", menuName = "Capybara-Rancher/FarmObject", order = 0)]
public class FarmObject : ScriptableObject {
    public GameObject Prefab;
    public int Price;
    public float[] PriceForUpgrade;
}