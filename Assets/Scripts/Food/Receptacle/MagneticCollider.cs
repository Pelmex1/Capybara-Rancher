using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticCollider : MonoBehaviour
{
    [SerializeField] private float speedOfMagnetism = 1f;
    [SerializeField] private Transform targetPosTransform;
    private Vector3 targetPos;
    private Transform targetObject;
    private void Start()
    {
        targetPos = targetPosTransform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        try
        {
            if (other.gameObject.GetComponent<MovebleObject>().data.isGenerable)
            {
                targetObject = other.transform;
                StartCoroutine(MagnetismToTargetPos());
            }
        }
        catch
        {

        }
    }
    IEnumerator MagnetismToTargetPos()
    {
        while(targetObject != null)
        {
            targetObject.position = Vector3.Lerp(targetObject.position, targetPos, speedOfMagnetism * Time.deltaTime);
            yield return null;
        }
    }
}
