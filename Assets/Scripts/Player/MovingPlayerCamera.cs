using UnityEngine;

public class MovingPlayer : MonoBehaviour
{
    [SerializeField] private Transform head;
    [SerializeField] private Transform gun;
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float energyRegenRate = 10f;
    [SerializeField] private float energyConsumptionRate = 5f;
    [SerializeField] private float energyMaxValue = 100f;

    private Rigidbody rb;
    private float vertical;
    private float horizontal;
    private float xRotationCamera;
    private Quaternion startHeadRotation;
    [SerializeField] private float energy;
    private bool isGrounded;
    private bool isRunning;
    private bool wasEnergyConsumed;
    private float energyOnLastFrame;

    public float mouseSensitivy;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        startHeadRotation = head.rotation;
        xRotationCamera = head.localRotation.eulerAngles.x;
        energy = energyMaxValue;
    }

    private void FixedUpdate()
    {
        float currentSpeed = isRunning ? speed * 1.8f : speed;

        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");
        rb.MovePosition(rb.position + vertical * currentSpeed * Time.fixedDeltaTime * transform.forward);
        rb.MovePosition(rb.position + horizontal * currentSpeed * Time.fixedDeltaTime * transform.right);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            if (isGrounded)
            {
                isGrounded = false;
                rb.AddForce(transform.up, ForceMode.Impulse);
            }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivy * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivy * Time.deltaTime;

        xRotationCamera -= mouseY;
        xRotationCamera = Mathf.Clamp(xRotationCamera, startHeadRotation.y - 3f, startHeadRotation.z + 30f);
        head.localRotation = Quaternion.Euler(xRotationCamera, 0, 0);
        transform.Rotate(Vector3.up * mouseX);

        if (!wasEnergyConsumed)
            Invoke(nameof(RegenerateEnergy), 3f);

        ConsumeEnergyByRunning();
        CheckConsumptionOfEnergy();
        isRunning = Input.GetKey(KeyCode.LeftShift) && energy > 0;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        isGrounded = true;
    }

    private void RegenerateEnergy()
    {
        if (!wasEnergyConsumed)
        {
            energy += energyRegenRate * Time.deltaTime;
            energy = Mathf.Clamp(energy, 0f, energyMaxValue);
        }
    }
    private void ConsumeEnergyByRunning()
    {
        if (isRunning)
        {
            energy -= energyConsumptionRate * Time.deltaTime;
            energy = Mathf.Clamp(energy, 0f, energyMaxValue);
        }
    }
    private void CheckConsumptionOfEnergy()
    {
        wasEnergyConsumed = energyOnLastFrame > energy;
        energyOnLastFrame = energy;
    }
}
