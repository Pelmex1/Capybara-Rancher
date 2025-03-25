using System.Collections.Generic;

namespace CapybaraRancher.Interfaces {
    internal interface ITransitionCrystallData
    {
        public bool WasChangeDict { get; set; }
        public Dictionary<string, int> DictionaryCrystall { get; set; }
    }
}