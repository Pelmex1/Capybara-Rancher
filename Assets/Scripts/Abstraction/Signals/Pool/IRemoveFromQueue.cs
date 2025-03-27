using CapybaraRancher.Enums;
using UnityEngine;

namespace CapybaraRancher.Abstraction.Signals.Pool
{
    public class IRemoveFromQueue
    {
        public GameObject GameObject;
        public string GameObjectName;
        public TypeGameObject Type;
        public IRemoveFromQueue(string name, TypeGameObject typeGameObject)
        {
            GameObjectName = name;
            Type = typeGameObject;
        }
    }
}