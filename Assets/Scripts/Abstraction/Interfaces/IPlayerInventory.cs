using CapybaraRancher.CustomStructures;

namespace CapybaraRancher.Interfaces 
{
    internal interface IInventoryPlayer
    {
        public Data[] Inventory { get; set; }
    }
}