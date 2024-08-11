using CapybaraRancher.Interfaces;
using UnityEngine;
namespace CapybaraRancher.UpgradeManagers {
    public class CattlePenManager : MonoBehaviour, ICattlePenManager
    {
        public bool[] Upgrades {get; set; } = new bool[2];
        private void Start() {
            for(int i = 0; i < Upgrades.Length; i++){
                if(PlayerPrefs.GetString($"{transform.parent.transform.parent.transform.parent.transform.parent.name}_Upgrade_{i}") == "true"){
                    Upgrades[i] = true;
                }
            }
        }
        //дальше должно быть активация апгрейда
    }
}
