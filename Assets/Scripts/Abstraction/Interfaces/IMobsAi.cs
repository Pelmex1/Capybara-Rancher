using UnityEngine;
namespace CapybaraRancher.Interfaces
{
    internal interface IMobsAi
    {
        void SetFoodFound(bool _input);
        void IsFoodFound(Transform foodTransform);
    }
}
