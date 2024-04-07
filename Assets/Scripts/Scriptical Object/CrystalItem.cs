using UnityEngine;

public class CrystalItem : MonoBehaviour, ICrystalItem
{
    [SerializeField] private GameObject nextCapibara;
    [SerializeField] private float price;
    [SerializeField] private string nameOfFavouriteFoodThisType;
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

    public string NameOfFavouriteFoodThisType
    {
        get { return nameOfFavouriteFoodThisType; }
        set { nameOfFavouriteFoodThisType = value; }
    }

    public FoodType WhatEatThisType
    {
        get { return whatEatThisType; }
        set { whatEatThisType = value; }
    }
}
