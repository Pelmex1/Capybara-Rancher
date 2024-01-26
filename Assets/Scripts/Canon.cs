using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private GameObject canonEnter;
    private int index = 0;
    private readonly float speed = 5f; 
    public List<GameObject> objectsInCollider;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("movebleObject")){
            objectsInCollider.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("movebleObject")){
            objectsInCollider.Remove(other.gameObject);
        }
    }
    private void FixedUpdate() {
        Vector3.Lerp(objectsInCollider[index].transform.position, canonEnter.transform.position, speed * Time.fixedDeltaTime);
        index++; 
    }
}
