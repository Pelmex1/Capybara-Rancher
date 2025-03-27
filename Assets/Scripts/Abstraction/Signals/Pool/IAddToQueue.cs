using CapybaraRancher.Enums;
using UnityEngine;

namespace CapybaraRancher.Abstraction.Signals.Pool
{
    public class IAddToQueue
    {
        public GameObject GameObject;
        public string GameObjectName;
        public TypeGameObject Type;
        public IAddToQueue(GameObject localGameObject, string name,TypeGameObject typeGameObject)
        {
            GameObject = localGameObject;
            GameObjectName = name;
            Type = typeGameObject;
        }
    }
}