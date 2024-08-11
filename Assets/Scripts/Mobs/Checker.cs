using CapybaraRancher.Interfaces;
using UnityEngine;

public class Checker : MonoBehaviour
{
    private ICrystallController crystallController;
    private bool inBuild = false;
    private void Start() {
        crystallController = GetComponent<ICrystallController>();
    }
    private void OnTriggerEnter(Collider other) {
        if(!inBuild && gameObject.CompareTag("Pen")){
            if(GetComponent<ICattlePenManager>().Upgrades[0]){
                crystallController.DelayBeforeCrystalSpawn /= 2;
                crystallController.DelayBeforeStarving /= 2;
                crystallController.StartCrystall += 1;
            }
            inBuild = true;
        }
    }
    private void OnTriggerStay(Collider other) {
        if(!inBuild && gameObject.CompareTag("Pen")){
            if(GetComponent<ICattlePenManager>().Upgrades[0]){
                crystallController.DelayBeforeCrystalSpawn /= 2;
                crystallController.DelayBeforeStarving /= 2;
                crystallController.StartCrystall += 1;
            }
            inBuild = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(inBuild && gameObject.CompareTag("Pen")){
            if(GetComponent<ICattlePenManager>().Upgrades[0]){
                crystallController.DelayBeforeCrystalSpawn *= 2;
                crystallController.DelayBeforeStarving *= 2;
                crystallController.StartCrystall -= 1;
            }
            inBuild = false;
        }
    }
}
