using System;
using UnityEngine;

namespace CustomEventBus
{
    public static class EventBus
    {
        #region Artem Action
        public static Predicate<InventoryItem> AddItemInInventory;
        //public static Action CanonIsEnum
        public static Action<MovebleObject> RemoveFromList;
        public static Action<bool> InumeratorIsEnabled;
        public static Action<bool> EnableHelpUi;
        #endregion

        public static Action ChnageGrassMod;

        public static Action OnLorScene;

        public static Action<string> LodingScene;
        public static Action<float[]> GetMusicValue;
        public static Action<float[]> SaveMusicValue;
        public static Action<int, Transform, GameObject, GameObject> TransitionBuildingData;
        public static Action OffBuilding;

        public static Action<int> WasChangeFarm;
    }

}
