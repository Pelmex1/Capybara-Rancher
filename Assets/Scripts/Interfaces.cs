using UnityEngine;

public interface ICapybaraAudioController
{
    void SetHappyStatus();
    void SetAngryStatus();
    void Eating();
}
public interface IMobsAi
{
    void SetFoodFound(bool _input);
    void IsFoodFound(Transform foodTransform);
}
public interface IItemActivator
{
    void ActivatorItemsAdd(GameObject addObject);
    void ActivatorItemsRemove(GameObject removeObject);
}
public interface IFoodItem
{
    int IndexForSpawnFarm { get; }
    float TimeGeneration { get; }
    bool IsGenerable { get; }
    FoodType Type { get; }
}
public interface ICrystalItem
{
    public GameObject NextCapibara { get; }
    public float Price { get; }
    public string NameOfFavouriteFoodThisType { get; }
    public FoodType WhatEatThisType { get; }
}
public interface ICapybaraItem
{
    public GameObject CrystalPrefab { get; }
    public FoodType WhatEat { get; }
    public string NameOfFavouriteFood { get; }
}