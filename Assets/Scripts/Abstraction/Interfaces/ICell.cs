using UnityEngine.UI;

namespace CapybaraRancher.Interfaces 
{
    internal interface ICell
    {
        public Image Image { get; set; }
        public InventoryItem InventoryItem { get; set; }
        public int Count { get; set; }
    }
}