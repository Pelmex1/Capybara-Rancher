using System.Collections.Generic;
using CapybaraRancher.CustomStructures;
using CapybaraRancher.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace CapybaraRancher.Interfaces
{
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
        public float PercentOfRegen { get; }
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
        public Image Image { get; set; }
        public InventoryItem InventoryItem { get; set; }
        public int Count { get; set; }
    }
    internal interface IMovebleObject
    {
        public InventoryItem Data { get; set; }
        public GameObject Localgameobject { get; set; }
    }
    internal interface IRobotParts
    {
        public int IndexofPart { get; set; }
        public bool CheckMoving { get; set; }
        public bool WasBuilding { get; set; }
        public GameObject[] AllPartsObject { get; set; }
        public void OnUI();
    }
    internal interface ICrystall
    {
        public string NameCrystal {get; set;}
    }
    internal interface ITransitionCrystallData
    {
        public bool WasChangeDict { get; set; }
        public Dictionary<string, int> DictionaryCrystall { get; set; }
    }
    internal interface IPlayer
    {
        public float Energy { get; set; }
        public float Health { get; set; }
        public float Hunger { get; set; }
        public float EnergyMaxValue { get; }
        public float HealthMaxValue { get; }
        public float HungerMaxValue { get; }
    }
    internal interface IObjectSpawner
    {
        public void ReturnToPool(GameObject returnObject);
    }
    internal interface IPooledMovebleObject
    {
        public TypeGameObject TypeGameObject { get; set; }
    }
    internal interface IInventoryPlayer
    {
        public Data[] Inventory {get; set;}
    }
}