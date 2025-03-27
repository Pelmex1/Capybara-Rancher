using CapybaraRancher.Abstraction.CustomStructures;
namespace CapybaraRancher.Abstraction.Signals.Chest
{
    public class IUpdateChestUI
    {
        public Data[] ChestCells;
        public Data[] InventoryCells;
        public IUpdateChestUI(Data[] chestCells,Data[] inventoryCells)
        {
            ChestCells = chestCells;
            InventoryCells = inventoryCells;
        }
    }
}