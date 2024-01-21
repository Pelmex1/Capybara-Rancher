using UnityEngine;

public class MovingPlayer : MonoBehaviour
{
    [SerializeField] private  GameObject head;
    [SerializeField] private float speed;
    private Rigidbody rb;
    private float vertical;
    private float horizontal;
    private void Start() {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate() {
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");
        rb.MovePosition(rb.position + vertical * speed * Time.fixedDeltaTime * transform.forward);
        //rb.MovePosition(rb.position + Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime * transform.up); <-- Jump
        rb.MovePosition(rb.position + horizontal * speed * Time.fixedDeltaTime * transform.right);
        
    }
    private void OnMouseDown() {
        Quaternion toRotation = Quaternion.LookRotation(Input.mousePosition, Vector3.up);
        Quaternion rotation = Quaternion.Lerp(rb.rotation, toRotation, speed * Time.deltaTime);
        rb.MoveRotation(rotation);
    }
}
