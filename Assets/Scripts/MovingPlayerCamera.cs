using UnityEngine;

public class MovingPlayer : MonoBehaviour
{
    [SerializeField] private Transform head;
    [SerializeField] private Transform gun;
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    private Rigidbody rb;
    private float vertical;
    private float horizontal;
    private float xRotationCamera;
    public float mouseSensitivy;
    private Quaternion startHeadRotation;
    private void Start() {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        startHeadRotation = head.rotation;
    }
    private void FixedUpdate() {
        
        
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");
        rb.MovePosition(rb.position + vertical * speed * Time.fixedDeltaTime * transform.forward);
        
        rb.MovePosition(rb.position + horizontal * speed * Time.fixedDeltaTime * transform.right);
        
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)){
            rb.MovePosition(rb.position + jumpSpeed * Time.deltaTime * transform.up);
        }
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivy * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivy * Time.deltaTime;
        
        xRotationCamera -= mouseY;
        xRotationCamera = Mathf.Clamp(xRotationCamera, startHeadRotation.y - 3f, startHeadRotation.z + 30f);
        head.localRotation = Quaternion.Euler(xRotationCamera, 0, 0);

        transform.Rotate(Vector3.up * mouseX);
    }
}
