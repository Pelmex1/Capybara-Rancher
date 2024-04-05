using System;

namespace CustomEventBus
{
    public static class EventBus
    {
/*         private EventBus() { }
        private static EventBus _eventBus;

        public static EventBus eventBus => _eventBus ?? (_eventBus = new EventBus()); */

        public static Action ChnageGrassMod;

        public static Action<string> LodingScene;
        public static Action<float[]> GetMusicValue;

        public static Action<float[]> SaveMusicValue;
    }

}
