using CapybaraRancher.Abstraction.CustomStructures;

namespace CapybaraRancher.Abstraction.Signals.Chest
{
    public class IChangeChestArray
    {
        public Indexer indexer;
        public IChangeChestArray(Indexer counter)
        {
            indexer = counter;
        }
    }
}

