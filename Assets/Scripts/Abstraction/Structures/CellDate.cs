using UnityEngine;

namespace CapybaraRancher.CustomStructures
{

    public class Data
    {
        public InventoryItem InventoryItem;
        public int Count;
        public Sprite Image;

        public static Data operator ++(Data cell)
        {
            return new Data { InventoryItem = cell.InventoryItem, Image = cell.InventoryItem.Image, Count = ++cell.Count };
        }
        public static Data operator --(Data cell)
        {
            return new Data { InventoryItem = cell.InventoryItem, Image = cell.Image, Count = --cell.Count };
        }
        public static explicit operator int(Data counter)
        {
            return counter.Count;
        }
        public override bool Equals(object obj)
        {
            if(obj == null && Image == null)
            {
                return true;
            }
            if(obj != null && obj is Data data)
            {
                if(data.InventoryItem == InventoryItem)
                {
                    return true;
                }
            }
            return false;
        }
        public override int GetHashCode()
        {
            throw new System.NotImplementedException();
        }
    }
}
