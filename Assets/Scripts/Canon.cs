using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    private readonly float speed = 5f; 
    private void Update() {
        //if(Input.GetMouseButton(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin, ray.direction * 50f, Color.red);

            if (Physics.Raycast(ray, out RaycastHit hit))
            { 
                Debug.Log(hit.transform.gameObject.name);
            }
        //}
    }
}
