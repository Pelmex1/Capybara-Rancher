using CapybaraRancher.Abstraction.CustomStructures;
using UnityEngine;
using UnityEngine.UI;

namespace CapybaraRancher.Abstraction.Signals.Chest
{
    public class IFoundPositionInChest
    {
        public Vector3 PositionNow;
        public Image image;
        public Indexer indexer;
        public IFoundPositionInChest(Vector3 PosNow, Image image)
        {
            PositionNow = PosNow;
            this.image = image;
        }
    }
}