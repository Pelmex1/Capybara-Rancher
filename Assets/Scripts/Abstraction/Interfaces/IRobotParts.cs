using UnityEngine;

namespace CapybaraRancher.Interfaces
{
    internal interface IRobotParts
    {
        public int IndexofPart { get; set; }
        public bool CheckMoving { get; set; }
        public bool WasBuilding { get; set; }
        public GameObject[] AllPartsObject { get; set; }
        public void OnUI();
    }
}