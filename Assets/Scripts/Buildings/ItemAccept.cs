using CapybaraRancher.EventBus;
using CapybaraRancher.Enums;
using CapybaraRancher.Interfaces;
using UnityEngine;

public class ItemAccept : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("movebleObject"))
        {
            if (other.gameObject.TryGetComponent<ICrystalItem>(out var crystalItem))
            {
                TypeGameObject crystalType = other.gameObject.GetComponent<IMovebleObject>().Data.TypeGameObject;
                EventBus.AddMoney(crystalItem.Price);
                EventBus.AddInPool(other.gameObject, crystalType);
                other.gameObject.SetActive(false);
                EventBus.SellTutorial.Invoke();
                EventBus.SelledCrystal.Invoke(crystalType);
            }
        }
    }
}
