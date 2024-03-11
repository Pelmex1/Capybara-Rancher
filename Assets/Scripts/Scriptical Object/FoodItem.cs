using UnityEngine;

public enum FoodType
{
    zero, All, Fruit, Vegetable, Meat
}
public class FoodItem : MonoBehaviour
{
    public int indexForSpawnFarm;
    public float timeGeneration;
    public bool isGenerable;
    public FoodType type;
}
