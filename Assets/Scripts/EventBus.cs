using System;
using CapybaraRancher.CustomStructures;
using CapybaraRancher.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace CapybaraRancher.EventBus
{
    public static class EventBus
    {
        public static Action GlobalSave;
        public static Func<string> GetSaveName;
        public static Action<string> SetSaveName;
        public static Action<int, bool> BuyFarm;
        public static Action<bool[], bool> UpdateFarmButtons;
        public static Action<bool> ActiveFarmPanel = delegate { };
        public static Action<bool> ActiveHelpText = delegate { };
        public static Func<FarmObject[]> GetBuildings;
        public static Action<string> AddInDisable;
        public static Action<string> RemoveFromDisable;
        public static Action<GameObject, TypeGameObject> AddInPool;
        public static Func<TypeGameObject, GameObject> RemoveFromThePool;
        public static Action<float> AddMoney;
        public static Action<GameObject> RemoveFromList;
        public static Predicate<GameObject> CheckList;
        public static Action<bool> InumeratorIsEnabled;
        public static Action<bool> EnableHelpUi;
        public static Predicate<InventoryItem> AddItemInInventory;
        public static Func<float> GetMoney;
        public static Action<TypeGameObject> SelledCrystal;
        #region  UIinventory
        public static Action<Data[]> OnRepaint;
        public static Action<int, int> WasChangeIndexCell;
        #endregion UIinventory

        #region  Options and Loading Scenes
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

        public static Action KeysRebinded;
        #endregion Options and Loaading Scenes

        #region  Building
        public static Func<FarmType[]> GetFarms;
        #endregion Building

        #region CapybaraSpawn
        public static Func<GameObject[]> SendCapybarasObjects;
        #endregion CapybaraSpawn

        #region AudioActions
        public static Action<bool, bool> PlayerMove;
        public static Action PlayerJump;
        public static Action PlayerGunRemove;
        public static Action PlayerGunAdd;
        public static Action<bool> PlayerGunAttraction;
        #endregion AudioActions

        #region OldRobotAction
        public static Action WasAddingAllCrystall;
        public static Action<string> SetNameCrystal = delegate { };
        public static Action<GameObject[]> TransitonPartsData;
        public static Action AddToDict;
        public static Action<int, Transform> OffMovebleObject;
        public static Action Win;
        #endregion OldRobotAction

        #region PlayerUpgrades
        public static Action MaxValueUpgrade;
        public static Action ExtraSlotUpgrade;
        public static Action EnergySpendingUpgrade;
        public static Action<bool> BuyJump;
        #endregion PlayerUpgrades

        #region Tutorial
        public static Action ScipTutorial = () => {};
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
        public static Action<Data[], Data[]> UpdateChestUI;
        public static Action<Transform> SetChestParent;
        public static Action<(int localIndex2, int localMindex, int newIndex, int newMIndex)> ChangeArray;
        public static Func<Vector3, Image, (int localIndex2, int localMindex, int newIndex, int newMIndex)> FoundPos;
        #endregion
        #region Input
        public static Action JumpInput;
        public static Action RunInput;
        public static Action PauseInput;
        public static Action EatInput;
        public static Action TerminalUseInput;
        public static Action InfoBookInput;
        public static Action PullInput;
        public static Action NonPullInput;
        public static Action ThrowInput;
        public static Action SatchelInput;
        public static Action<KeyCode> ChangeKey;
        #endregion
    }

}
