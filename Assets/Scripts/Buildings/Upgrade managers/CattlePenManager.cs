using CapybaraRancher.Interfaces;
using UnityEngine;
namespace CapybaraRancher.UpgradeManagers {
    public class CattlePenManager : MonoBehaviour, IUpgradeManager
    {
        public bool[] Upgrades {get; set; } = new bool[2];
        private void Awake() {
            for(int i = 0; i < Upgrades.Length; i++){
                if(PlayerPrefs.GetString($"{transform.parent.transform.parent.transform.parent.transform.parent.name}_Upgrade_{i}") == "true"){
                    Upgrades[i] = true;
                }
            }
        }
        public int UpUpgrade(){
            for(int i = 0; i < Upgrades.Length; i++){
                if(!Upgrades[i]){
                    switch(i){
                        case 1:
                        FirstUpgrade();
                        Upgrades[0] = true;
                        break;
                        case 2:
                        SecondUpgrade();
                        Upgrades[1] = true;
                        break;
                    }
                
                }
            }
            return -1;
        }
        private int FirstUpgrade(){
            Upgrades[0] = true;
            return 0;
        }
        private int SecondUpgrade(){
            return 1;

        }
    }
}
