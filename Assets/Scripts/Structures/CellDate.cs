using UnityEngine.UI;

namespace CapybaraRancher.CustomStructures
{
    public static class CellData
    {
        public struct Data
        {
            public InventoryItem InventoryItem;
            public int Count;
            public Image Image;
            
            public static Data operator ++(Data cell)
            {
                return new Data { InventoryItem = cell.InventoryItem, Image = cell.Image, Count = cell.Count++ };
            }
                public static Data operator --(Data cell)
            {
                return new Data { InventoryItem = cell.InventoryItem, Image = cell.Image, Count = cell.Count-- };
            }
            public static explicit operator int(Data counter)
            {
                return counter.Count;
            }
            public static bool operator == (Data cell1, Data cell2)
            {
                /*if(cell2 == null)
                {
                    if(cell1.Count == 0 && cell1.InventoryItem == null)
                    {
                        return true;
                    } else return false;
                } else if(cell1.Count == cell2.Count && cell1.InventoryItem == cell2.InventoryItem)
                {
                    return true;
                } else return false;*/
                if(cell1.Equals(cell2)) 
                {
                    return true;
                } else return false;
            }
            public static bool operator !=(Data cell1, Data cell2)
            {
                if(!cell1.Equals(cell2)) 
                {
                    return true;
                } else return false;
            }
        }
    }
}
