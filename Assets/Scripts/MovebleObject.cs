using UnityEngine;
using UnityEngine.Events;

public class MovebleObject : MonoBehaviour
{
    private UnityEvent OnMove;
    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        OnMove.AddListener(OnTrigeredd());
    }
    
    private void OnTrigeredd(Transform position){
        rb.AddForce(position.position, ForceMode.Impulse);
    }

}
