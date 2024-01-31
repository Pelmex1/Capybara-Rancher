using UnityEngine;

public class CanonCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("movebleObject")){
            // In inventory function here
            Destroy(other.gameObject);
        };
    }
}
