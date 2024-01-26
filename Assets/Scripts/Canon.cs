using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private GameObject canonEnter;
    private int index = 0;
    private readonly float speed = 5f; 
    public List<Transform> objectsInCollider;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("movebleObject")){
            objectsInCollider.Add(other.gameObject.transform);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("movebleObject")){
            objectsInCollider.Remove(other.gameObject.transform);
        }
    }
    private void FixedUpdate() {
        if(index == objectsInCollider.Count - 1) index = 0;
        Vector3.Lerp(objectsInCollider[index].position, canonEnter.transform.position, speed * Time.fixedDeltaTime);
        index++;
    }
}
