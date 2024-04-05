using System;

namespace CustomEventBus
{
    public static class EventBus
    {
/*         private EventBus() { }
        private static EventBus _eventBus;

        public static EventBus eventBus => _eventBus ?? (_eventBus = new EventBus()); */
        #region Artem Action
        public static Predicate<InventoryItem> AddItemInInventory;
        //public static Action CanonIsEnum
        public static Action<MovebleObject> RemoveFromList;
        public static Action<bool> InumeratorIsEnabled;
        public static Action<bool> EnableHelpUi;
        #endregion

        public static Action ChnageGrassMod;

        public static Action<string> LodingScene;
        public static Action<float[]> GetMusicValue;

        public static Action<float[]> SaveMusicValue;
    }

}
