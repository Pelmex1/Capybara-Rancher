using CapybaraRancher.Abstraction.Signals.Chest;
using CustomEventBus;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChestCell : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Image _image;
    private Vector3 _pos;
    private Transform _parentTransform;
    private EventBus _eventBus;
    
    private void Start()
    {
        _pos = transform.position;
        _parentTransform = transform.parent;
    }
    public void OnDrag(PointerEventData pointerEventData)
    {
        transform.position = pointerEventData.pointerCurrentRaycast.screenPosition;
    }
    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        _eventBus.Invoke<ISetChestParent>(new(transform));
    }
    public void OnEndDrag(PointerEventData pointerEventData)
    { 
        IFoundPositionInChest foundPositionInChestClass = new(transform.position, _image);
        _eventBus.Invoke(foundPositionInChestClass);
        _eventBus.Invoke<IChangeChestArray>(new(foundPositionInChestClass.indexer));
        transform.position = _pos;
        transform.SetParent(_parentTransform);
    }
    void OnEnable()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
    }
}
