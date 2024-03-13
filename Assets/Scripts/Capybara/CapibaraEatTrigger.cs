using UnityEngine;

public class CapibaraEatTrigger : MonoBehaviour
{
    private MobsAi mobsAi;
    private CrystalsController crystalsController;
    private void Start() 
    {
        mobsAi = GetComponentInParent<MobsAi>();
        crystalsController = GetComponentInParent<CrystalsController>();
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.GetComponent<FoodItem>() && crystalsController){
            mobsAi.IsFoodFound(other.transform);
        }
    }
}
