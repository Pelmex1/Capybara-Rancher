using UnityEngine;

namespace CapybaraRancher.Abstraction.Signals.Chest
{
    public class ISetChestParent
    {
        public Transform transform;
        public ISetChestParent(Transform value)
        {
            transform = value;
        }
    }
}