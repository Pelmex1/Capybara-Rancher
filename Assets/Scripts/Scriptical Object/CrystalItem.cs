using CustomEventBus;
using UnityEngine;

public class CrystalItem : MonoBehaviour, ICrystalItem
{
    [SerializeField] private GameObject nextCapibara;
    [SerializeField] private float price;
    [SerializeField] private float percentOfRegen;
    [SerializeField] private string favouriteFoodName;
    [SerializeField] private FoodType whatEatThisType;
    public GameObject NextCapibara
    {
        get { return nextCapibara; }
        set { nextCapibara = value; }
    }

    public float Price
    {
        get { return price; }
        set { price = value; }
    }

    public float PercentOfRegen
    {
        get { return percentOfRegen; }
        set { percentOfRegen = value; }
    }

    public string FavouriteFoodName
    {
        get { return favouriteFoodName; }
        set { favouriteFoodName = value; }
    }

    public FoodType WhatEatThisType
    {
        get { return whatEatThisType; }
        set { whatEatThisType = value; }
    }
}
