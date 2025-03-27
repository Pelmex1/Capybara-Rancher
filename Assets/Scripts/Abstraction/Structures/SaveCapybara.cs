using UnityEngine;

namespace CapybaraRancher.Abstraction.CustomStructures
    {
    public class SaveCapybara
    {
        public string Name;
        public Vector3 Position;
        public CapybaraData Data1;
        public CapybaraData Data2;
        public CapybaraItem CapybaraItem;
        public InventoryItem Item;

        public SaveCapybara(string name, Vector3 position, CapybaraData data1, CapybaraData data2, CapybaraItem capybaraItem, InventoryItem item)
        {
            Name = name;
            Position = position;
            Data1 = data1;
            Data2 = data2;
            CapybaraItem = capybaraItem;
            Item = item;
        }
    }
}