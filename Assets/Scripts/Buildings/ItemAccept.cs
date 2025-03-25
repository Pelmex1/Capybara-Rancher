using CapybaraRancher.EventBus;
using CapybaraRancher.Enums;
using CapybaraRancher.Interfaces;
using UnityEngine;
using CapybaraRancher.Consts;

public class ItemAccept : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.MOVEBLE_OBJECT_TAG))
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
