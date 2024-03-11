using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAccept : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("movebleObject")){
            if(other.gameObject.TryGetComponent<CrystalItem>(out var crystalItem))
            {
                Iinstance.instance.money += crystalItem.price;
                Destroy(other.gameObject);
            }
        }
    }
}
