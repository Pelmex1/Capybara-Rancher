using UnityEngine;

public class CanonCollider : MonoBehaviour
{
    private Canon canonScript;
    private void Start() {
        canonScript = GetComponent<Canon>();
    }
    private void OnTriggerEnter(Collider other) {
        if(canonScript.objectsInCollider.Contains(gameObject)){
            // In inventory function here
            Destroy(other.gameObject);
        };
    }
}
