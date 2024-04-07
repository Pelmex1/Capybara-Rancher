using UnityEngine;

public enum FoodType
{
    zero, All, Fruit, Vegetable, Meat
}
public class FoodItem : MonoBehaviour, IFoodItem
{
    [SerializeField] private int indexForSpawnFarm;
    [SerializeField] private float timeGeneration;
    [SerializeField] private bool isGenerable;
    [SerializeField] private FoodType type;

    public int IndexForSpawnFarm
    {
        get { return indexForSpawnFarm; }
        set { indexForSpawnFarm = value; }
    }

    public float TimeGeneration
    {
        get { return timeGeneration; }
        set { timeGeneration = value; }
    }

    public bool IsGenerable
    {
        get { return isGenerable; }
        set { isGenerable = value; }
    }

    public FoodType Type
    {
        get { return type; }
        set { type = value; }
    }
}
