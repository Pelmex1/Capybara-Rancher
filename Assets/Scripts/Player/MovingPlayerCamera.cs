using UnityEngine;

public class MovingPlayer : MonoBehaviour
{
    [SerializeField] private Transform head;
    [SerializeField] private float speed;

    private Rigidbody rb;
    private Quaternion startHeadRotation;
    private readonly float energyRegenRate = 5f;
    private readonly float energyConsumptionRate = 10f;
    private float vertical;
    private float horizontal;
    private float xRotationCamera;
    private float startSpeed;
    private bool isRunning = false;
    private bool isGrounded;

    public float mouseSensitivy;
    public float energy;
    public float hp = 100f;
    public readonly float energyMaxValue = 50f;
    public readonly float hpMaxValue = 100f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        startHeadRotation = head.rotation;
        xRotationCamera = head.localRotation.eulerAngles.x;
        energy = energyMaxValue;
        startSpeed = speed;
    }
    private void OnCollisionEnter(Collision other)
    {
        isGrounded = true;
    }

    private void FixedUpdate()
    {
        if(isGrounded)
        {
            vertical = Input.GetAxisRaw("Vertical");
            horizontal = Input.GetAxisRaw("Horizontal");
        }
        rb.MovePosition(rb.position + vertical * speed * Time.fixedDeltaTime * transform.forward);
        rb.MovePosition(rb.position + horizontal * speed * Time.fixedDeltaTime * transform.right);
        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            if (isGrounded)
            {
                isGrounded = false;
                rb.AddForce(transform.up, ForceMode.Impulse);
            }
        if(Cursor.lockState == CursorLockMode.Locked){
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivy * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivy * Time.deltaTime;

            xRotationCamera -= mouseY;
            xRotationCamera = Mathf.Clamp(xRotationCamera, startHeadRotation.y - 85f, startHeadRotation.z + 40f);
            head.localRotation = Quaternion.Euler(xRotationCamera, 0, 0);
            transform.Rotate(Vector3.up * mouseX);
        }

        if(energy > 5 && Input.GetKey(KeyCode.LeftShift)){
            energy -= energyConsumptionRate * Time.deltaTime;
            speed = startSpeed * 1.8f;
            isRunning = true;
        }else if(!(energy > 5) && isRunning){
            energy = 0;
            isRunning = false;
        } else if (energy < energyMaxValue){
            energy += energyRegenRate * Time.deltaTime;
            speed = startSpeed;
        }
    }
}
