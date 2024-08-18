using System.Collections;
using System.Collections.Generic;
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
        if(other.gameObject.tag == "movebleObject" & other.gameObject.GetComponent<IFoodItem>() != null)
        {
            mobsAi.IsFoodFound(other.transform);
        }
    }
}
