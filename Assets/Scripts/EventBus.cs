using System;
using UnityEngine;
using UnityEngine.UI;

namespace CustomEventBus
{
    public static class EventBus
    {
        #region Artem Action
        public static Func<Sprite> GetDefaultSprite;
        public static Func<InventoryItem> GetDefaultItem;
        public static Action<float> AddMoney;
        public static Action<GameObject> RemoveFromList;
        public static Predicate<GameObject> CheckList;
        public static Action<bool> InumeratorIsEnabled;
        public static Action<bool> EnableHelpUi;
        public static Predicate<InventoryItem> AddItemInInventory;
        public static Func<float> GetMoney;
        public static Action<InventoryItem, Sprite, int, int> SetCellsData;
        #region Inventory
        public static Func<int, int> GetInt;
        public static Func<int, InventoryItem> GetInventoryItem;
        public static Action<IInventory> AddImageInInventory;
        #endregion
        #endregion

        #region  Options and Loaading Scenes
        public static Action ChnageGrassMod;

        public static Action OnLorScene;

        public static Action<string> LodingScene;
        public static Action<float> WasChangeMouseSensetive;
        public static Action<float[]> GetMusicValue;
        public static Action<float[]> SaveMusicValue;      
        public static Action<float, float> GetEnergyPlayerData;
        public static Action<float, float> GiveEnergyPlayerData;
        #endregion Options and Loaading Scenes

        #region  Building
        public static Action<int, Transform, GameObject, GameObject> TransitionBuildingData;
        public static Action OffBuilding;
        public static Action<int> WasChangeFarm;
        #endregion Building

        #region Audio Action
        public static Action<bool, bool> PlayerMove;
        public static Action PlayerJump;
        public static Action PlayerGunRemove;
        public static Action PlayerGunAdd;
        public static Action PlayerGunAttraction;
        #endregion
    }

}
