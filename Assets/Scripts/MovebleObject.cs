using UnityEngine;

public class MovebleObject : MonoBehaviour
{
    private readonly float speed = 0.1f;
    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }
    
    public void OnTrigeredd(Transform pos){
        transform.position = Vector3.SlerpUnclamped(transform.position, pos.position, speed * Time.deltaTime);
    }
    private void OnDestroy() {
        Canon.ItemCollection -= OnTrigeredd;
    }
}
