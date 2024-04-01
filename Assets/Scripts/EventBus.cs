using UnityEngine;
using System;

public class EventBus
{
    private EventBus()
    {
        
    }
    private static EventBus _eventBus;

    public static EventBus eventBus => _eventBus ?? (_eventBus = new EventBus());

    public Action ChnageGrassMod;
    public Action<float[]> GetMisicValue;

    public Action<float[]> SaveMusicValue;
}
