using UnityEngine;

public class MovingPlayer : MonoBehaviour
{
    [SerializeField] private Transform head;
    [SerializeField] private Transform gun;
    [SerializeField] private float speed;
    private Rigidbody rb;
    private float vertical;
    private float horizontal;
    private float xRotationCamera;
    private float xRotationGun;
    public float mouseSensitivy;
    private Quaternion startHeadRotation;
    private Quaternion startGunRotation;
    private void Start() {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        startHeadRotation = head.rotation;
        startGunRotation = gun.rotation;
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
        
        xRotationCamera -= mouseY;
        xRotationCamera = Mathf.Clamp(xRotationCamera, startHeadRotation.y - 100f, startHeadRotation.z + 90f);
        head.localRotation = Quaternion.Euler(xRotationCamera, 0f, 0f);

        //xRotationGun -= mouseY;
        //xRotationGun = Mathf.Clamp(xRotationGun, x, 0f);
        //gun.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }
}
