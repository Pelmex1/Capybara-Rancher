using UnityEngine;
using CapybaraRancher.Enums;

[CreateAssetMenu(fileName = "CapybaraData", menuName = "Capybara-Rancher/CapybaraData")]
[System.Serializable]
public class CapybaraData : ScriptableObject
{
    [SerializeField] private GameObject crystalPrefab;
    [SerializeField] private TypeGameObject crystalType;
    [SerializeField] private FoodType whatEat;
    [SerializeField] private string favouriteFood;
    [SerializeField] private GameObject mod;

    public GameObject CrystalPrefab
    {
        get { return crystalPrefab; }
        set { crystalPrefab = value; }
    }

    public TypeGameObject CrystalType
    {
        get { return crystalType; }
        set { crystalType = value; }
    }

    public FoodType WhatEat
    {
        get { return whatEat; }
        set { whatEat = value; }
    }

    public string FavouriteFood
    {
        get { return favouriteFood; }
        set { favouriteFood = value; }
    }

    public GameObject Mod
    {
        get { return mod; }
        set { mod = value; }
    }
}
