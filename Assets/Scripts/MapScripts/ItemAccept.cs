using CapybaraRancher.EventBus;
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
                EventBus.AddMoney(crystalItem.Price);
                EventBus.AddInPool(other.gameObject, other.gameObject.GetComponent<IMovebleObject>().Data.TypeGameObject);
                other.gameObject.SetActive(false);
                EventBus.SellTutorial.Invoke();
            }
        }
    }
}
