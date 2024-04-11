using UnityEngine;
using UnityEngine.UI;

internal interface ICapybaraAudioController
{
    void SetHappyStatus();
    void SetAngryStatus();
    void Eating();
}
internal interface IMobsAi
{
    void SetFoodFound(bool _input);
    void IsFoodFound(Transform foodTransform);
}
internal interface IFoodItem
{
    int IndexForSpawnFarm { get; }
    float TimeGeneration { get; }
    bool IsGenerable { get; }
    FoodType Type { get; }
}
internal interface ICrystalItem
{
    public GameObject NextCapibara { get; }
    public float Price { get; }
    public string FavouriteFoodName { get; }
    public FoodType WhatEatThisType { get; }
}
internal interface ICapybaraItem
{
    public GameObject CrystalPrefab { get; }
    public FoodType WhatEat { get; }
    public string NameOfFavouriteFood { get; }
}
internal interface ICell
{
    public Image Image {get; set;}
    public InventoryItem InventoryItem {get; set;}
    public int Count {get; set;}
}
internal interface IMovebleObject
{
    public InventoryItem Data {get; set;}
    public GameObject Localgameobject {get; set;}
    public void SetLocalObject();
}
internal interface IReceptacle
{
    public void GetData(Transform ParentPosition, GameObject NewObject);
}
internal interface IMovingPlayer
{
    public float Energy { get; set; }
    public float Hp { get; set; }
    public float EnergyMaxValue { get; }
    public float HpMaxValue { get; }
}
internal interface IMobsSpawner
{
    public void ReturnToPool(GameObject returnObject);
}