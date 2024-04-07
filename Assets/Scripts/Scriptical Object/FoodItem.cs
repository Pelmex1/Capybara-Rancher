using UnityEngine;

public enum FoodType
{
    zero, All, Fruit, Vegetable, Meat
}
public class FoodItem : MonoBehaviour
{
    public int IndexForSpawnFarm;
    public float TimeGeneration;
    public bool IsGenerable;
    public FoodType Type;
}
