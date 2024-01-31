using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private Transform canonEnter;
    public delegate void Moving(Transform transform);
    public static event Moving ItemCollection;
    private void Update() {
        if(Input.GetMouseButton(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin, ray.direction * 50f, Color.red);

            if (Physics.Raycast(ray, out RaycastHit hit))
            { 
                if(hit.collider.gameObject.CompareTag("movebleObject")){
                    ItemCollection += hit.collider.gameObject.GetComponent<MovebleObject>().OnTrigeredd;
                }
            }
            ItemCollection?.Invoke(canonEnter);
        }
    }
}
