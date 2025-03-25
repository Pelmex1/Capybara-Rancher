using UnityEngine;

namespace CapybaraRancher.Interfaces
{   
    internal interface IButtonSave
    {
        public string NameOfSave { get; set; }
        public Sprite IconOfSave { get; set; }
        public void InitSavePaenl();
    }
}