using UnityEngine;

namespace CapybaraRancher.Interfaces
{
    internal interface IObjectSpawner
    {
        void ReturnToPool(GameObject returnObject);

    }
}