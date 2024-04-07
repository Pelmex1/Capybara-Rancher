using UnityEngine;

public class CapybaraItem : MonoBehaviour, ICapybaraItem
{
    [SerializeField] private GameObject crystalPrefab;
    [SerializeField] private FoodType whatEat;
    [SerializeField] private string nameOfFavouriteFood;

    public GameObject CrystalPrefab
    {
        get { return crystalPrefab; }
        set { crystalPrefab = value; }
    }

    public FoodType WhatEat
    {
        get { return whatEat; }
        set { whatEat = value; }
    }

    public string NameOfFavouriteFood
    {
        get { return nameOfFavouriteFood; }
        set { nameOfFavouriteFood = value; }
    }
}
