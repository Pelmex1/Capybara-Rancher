namespace CapybaraRancher.Abstraction.CustomStructures
{
    public class Indexer
    {
        public int OldInventoryIndex;
        public int NewInventoryIndex;
        public int OldChestIndex;
        public int NewChestIndex;
        public Indexer(int OldInventoryIndex,int NewInventoryIndex,int OldChestIndex,int NewChestIndex)
        {
            this.OldInventoryIndex = OldInventoryIndex;
            this.NewInventoryIndex = NewInventoryIndex;
            this.OldChestIndex = OldChestIndex;
            this.NewChestIndex = NewChestIndex;
        }
    }
}