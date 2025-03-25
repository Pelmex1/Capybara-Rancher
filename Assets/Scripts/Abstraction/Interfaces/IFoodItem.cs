using CapybaraRancher.Enums;
namespace CapybaraRancher.Interfaces 
{
    internal interface IFoodItem
    {
        int IndexForSpawnFarm { get; }
        float TimeGeneration { get; }
        bool IsGenerable { get; }
        FoodType Type { get; }
    }
}