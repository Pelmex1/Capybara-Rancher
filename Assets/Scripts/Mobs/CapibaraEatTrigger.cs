using CapybaraRancher.Interfaces;
using UnityEngine;

public class CapibaraEatTrigger : MonoBehaviour
{
    private IMobsAi mobsAi;
    private void Start() 
    {
        mobsAi = GetComponentInParent<IMobsAi>();
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.GetComponent<IFoodItem>() != null)
        {
            mobsAi.IsFoodFound(other.transform);
        }
    }
}
