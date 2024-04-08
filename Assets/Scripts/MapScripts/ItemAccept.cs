using CustomEventBus;
using UnityEngine;

public class ItemAccept : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("movebleObject"))
        {
            if (other.gameObject.TryGetComponent<ICrystalItem>(out var crystalItem))
            {
                EventBus.AddMoney(crystalItem.Price);
                Destroy(other.gameObject);
            }
        }
    }
}
