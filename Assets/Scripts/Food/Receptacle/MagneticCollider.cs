using System.Collections;
using CapybaraRancher.Interfaces;
using UnityEngine;

public class MagneticCollider : MonoBehaviour
{
    [SerializeField] private float _speedOfMagnetism = 1f;
    [SerializeField] private Transform _targetPosTransform;

    private Vector3 _targetPos;
    private void Start()
    {
        _targetPos = _targetPosTransform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IFoodItem foodItem))
            if (foodItem.IsGenerable)
                StartCoroutine(MagnetismToTargetPos(other.transform));
    }
    IEnumerator MagnetismToTargetPos(Transform thisTargetObject)
    {
        while(thisTargetObject != null)
        {
            thisTargetObject.position = Vector3.Lerp(thisTargetObject.position, _targetPos, _speedOfMagnetism * Time.deltaTime);
            yield return null;
        }
    }
}
