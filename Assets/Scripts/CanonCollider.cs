using UnityEngine;

public class CanonCollider : MonoBehaviour
{
    [SerializeField] private Canon canonScript;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("movebleObject")){
            // In inventory function here
            canonScript.objectsInCollider.Remove(other.gameObject.transform);
            Destroy(other.gameObject);
        };
    }
}
