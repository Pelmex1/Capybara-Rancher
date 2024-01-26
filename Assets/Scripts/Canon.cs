using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private GameObject canonEnter;
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
        if(objectsInCollider.Count != 0){
            if(Input.GetMouseButton(0)){
                for(int i = 0; i < objectsInCollider.Count; i++){
                    Debug.Log("sssss");
                    //Vector3.Lerp(objectsInCollider[i].position, canonEnter.transform.position, speed * Time.deltaTime);
                    Vector3.Lerp(canonEnter.transform.position,objectsInCollider[i].position, speed * Time.deltaTime);
                }
            }
        }
    }
}
