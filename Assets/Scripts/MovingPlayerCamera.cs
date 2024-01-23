using UnityEngine;

public class MovingPlayer : MonoBehaviour
{
    [SerializeField] private  GameObject head;
    [SerializeField] private float speed;
    private Rigidbody rb;
    private float vertical;
    private float horizontal;
    private float xRotation = -90f;
    public float mouseSensitivy;
    private void Start() {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void FixedUpdate() {
        
        
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");
        rb.MovePosition(rb.position + vertical * speed * Time.fixedDeltaTime * transform.forward);
        //rb.MovePosition(rb.position + Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime * transform.up); <-- Jump
        rb.MovePosition(rb.position + horizontal * speed * Time.fixedDeltaTime * transform.right);
        
    }
    private void Update() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivy * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivy * Time.deltaTime;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -100f, 0f);

        head.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
