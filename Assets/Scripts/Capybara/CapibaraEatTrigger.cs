using UnityEngine;

public class CapibaraEatTrigger : MonoBehaviour
{
    private MobsAi mobsAi;
    private void Start() 
    {
        mobsAi = GetComponentInParent<MobsAi>();
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.GetComponent<FoodItem>())
        {
            mobsAi.IsFoodFound(other.transform);
        }
    }
}
