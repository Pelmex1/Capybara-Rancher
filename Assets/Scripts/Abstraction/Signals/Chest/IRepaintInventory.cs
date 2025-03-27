using CapybaraRancher.Abstraction.CustomStructures;

namespace CapybaraRancher.Abstraction.Signals.Chest
{
    public class IRepaintInventory
    {
        public Data[] Inventory;
        public IRepaintInventory(Data[] inventory)
        {
            Inventory = inventory;
        }
    }
}