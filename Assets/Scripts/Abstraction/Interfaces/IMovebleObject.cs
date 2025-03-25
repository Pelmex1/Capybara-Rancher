using UnityEngine;

namespace CapybaraRancher.Interfaces
{
    internal interface IMovebleObject
    {
        public InventoryItem Data { get; set; }
        public GameObject Localgameobject { get; set; }
    }
}