using System.Collections;
using UnityEngine;

public class MagneticCollider : MonoBehaviour
{
    [SerializeField] private float speedOfMagnetism = 1f;
    [SerializeField] private Transform targetPosTransform;
    private Vector3 targetPos;

    private void Start()
    {
        targetPos = targetPosTransform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        FoodItem foodItem;
        other.gameObject.TryGetComponent(out foodItem);
        if (foodItem)
            if (foodItem.isGenerable)
                StartCoroutine(MagnetismToTargetPos(other.transform));
    }
    IEnumerator MagnetismToTargetPos(Transform thisTargetObject)
    {
        while(thisTargetObject != null)
        {
            thisTargetObject.position = Vector3.Lerp(thisTargetObject.position, targetPos, speedOfMagnetism * Time.deltaTime);
            yield return null;
        }
    } 
}
