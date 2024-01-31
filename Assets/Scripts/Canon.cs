using UnityEngine;

public class Canon : MonoBehaviour
{
    private readonly float speed = 5f; 
    private void Update() {
        if(Input.GetMouseButton(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin * 50f, ray.direction * 50f, Color.red);

            if (Physics.Raycast(ray, out RaycastHit hit))
            { 
                if(hit.collider.gameObject.CompareTag("movebleObject")){
                    //sss
                }
            }
        }
    }
}
