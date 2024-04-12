using UnityEngine.UI;

namespace CapybaraRancher.CustomStructures
{

        public struct Data
        {
            public InventoryItem InventoryItem;
            public int Count;
            public Image Image;
            
            public static Data operator ++(Data cell)
            {
                return new Data { InventoryItem = cell.InventoryItem, Image = cell.Image, Count = ++cell.Count };
            }
                public static Data operator --(Data cell)
            {
                return new Data { InventoryItem = cell.InventoryItem, Image = cell.Image, Count = --cell.Count };
            }
            public static explicit operator int(Data counter)
            {
                return counter.Count;
            }
        }
}
