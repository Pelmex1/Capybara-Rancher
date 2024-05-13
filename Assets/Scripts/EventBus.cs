using System;
using CapybaraRancher.CustomStructures;
using CapybaraRancher.Enums;
using UnityEngine;

namespace CapybaraRancher.EventBus
{
    public static class EventBus
    {
        public static Action<int, bool> BuyFarm;
        public static Action<bool[]> UpdateFarmButtons;
        public static Action<bool> ActiveFarmPanel;
        public static Action<bool> ActiveHelpText;
        public static Func<FarmObject[]> GetBuildings;
        public static Action<GameObject> AddInDisable;
        public static Action<GameObject> RemoveFromDisable;
        public static Action<GameObject, TypeGameObject> AddInPool;
        public static Func<TypeGameObject, GameObject> RemoveFromThePool;
        public static Action<float> AddMoney;
        public static Action<GameObject> RemoveFromList;
        public static Predicate<GameObject> CheckList;
        public static Action<bool> InumeratorIsEnabled;
        public static Action<bool> EnableHelpUi;
        public static Predicate<InventoryItem> AddItemInInventory;
        public static Func<float> GetMoney;

        #region  UIinventory
        public static Action<Data[]> OnRepaint;
        public static Action<int, int> WasChangeIndexCell;
        #endregion UIinventory

        #region  Options and Loaading Scenes
        public static Action ChnageGrassMod;
        public static Action ChangeRendering;

        public static Action OnLorScene;

        public static Action<string> LodingScene;
        public static Action<float> WasChangeMouseSensetive;
        public static Action<float[]> GetMusicValue;
        public static Action<float[]> SaveMusicValue;
        public static Action GetEnergyPlayerData;
        public static Action<float, float, float> GiveEnergyPlayerData;

        public static Action PlayerRespawned;
        #endregion Options and Loaading Scenes

        #region  Building
        public static Action<int> WasChangeFarm;
        #endregion Building

        #region Audio Action
        public static Action<bool, bool> PlayerMove;
        public static Action PlayerJump;
        public static Action PlayerGunRemove;
        public static Action PlayerGunAdd;
        public static Action<bool> PlayerGunAttraction;
        #endregion
        #region OldRobotAction
        public static Action WasAddingAllCrystall;
        public static Action<string> SetNameCrystal = delegate { };
        public static Action<GameObject[]> TransitonPartsData;
        public static Action AddToDict;
        public static Action<string, int> OnMovebleObject;
        public static Action<int, Transform> OffMovebleObject;
        #endregion OldRobotAction
        
        #region PlayerUpgrades
        public static Action MaxValueUpgrade;
        public static Action ExtraSlotUpgrade;
        public static Action EnergySpendingUpgrade;
        #endregion PlayerUpgrades

        #region Tutorial
        public static Action MovingTutorial = () => { };
        public static Action InventoryTutorial = () => { };
        public static Action FeedTutorial = () => { };
        public static Action TransformationTutorial = () => { };
        public static Action SellTutorial = () => { };
        public static Action EatTutorial = () => { };
        public static Action BuildTutorial = () => { };
        #endregion Tutorial
        #region Chest
        public static Action<bool> EnableChestUi;
        public static Action<Data[],Data[]> UpdateChestUI;
        public static Action<Transform> SetChestParent;
        #endregion
    }

}
