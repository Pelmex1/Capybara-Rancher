using UnityEngine;
using CapybaraRancher.Interfaces;
using System.Collections;
using CapybaraRancher.EventBus;
using CapybaraRancher.Consts;

public class MovebleObject : MonoBehaviour, IMovebleObject
{
    [SerializeField] private InventoryItem inventoryItem;

    public InventoryItem Data { get => inventoryItem; set => inventoryItem = value; }
    public GameObject Localgameobject { get => gameObject; set { return; } }


    protected bool _isDisabled = false;
    protected bool _looted = false;
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.CANON_TAG) && !_looted && !_isDisabled)
        {
            if (EventBus.AddItemInInventory(Data))
            {
                _looted = true;
                EventBus.RemoveFromList(gameObject);
                gameObject.SetActive(false);
            }
        }
    }
    protected virtual IEnumerator Disabled()
    {
        yield return new WaitForSecondsRealtime(2);
        _isDisabled = false;
    }
    protected virtual void OnDisable()
    {
        _looted = false;
        EventBus.AddInPool(gameObject, Data.TypeGameObject);
    }
    protected virtual void OnEnable()
    {
        _isDisabled = true;
        StartCoroutine(Disabled());
    }
}
