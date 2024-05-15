using CapybaraRancher.EventBus;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChestCell : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Image _image;
    private Vector3 _pos;
    private Transform _parentTransform;
    
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
        EventBus.SetChestParent(transform);
    }
    public void OnEndDrag(PointerEventData pointerEventData)
    { 
        EventBus.ChangeArray.Invoke(EventBus.FoundPos(transform.position, _image));
        transform.position = _pos;
        transform.SetParent(_parentTransform);
    }
    
}
