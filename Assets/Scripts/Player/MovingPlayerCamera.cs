using CustomEventBus;
using UnityEngine;

public class MovingPlayer : MonoBehaviour
{
    private const float ROTATION_CAMERA_MISTAKEY = 85f;
    private const float ROTATION_CAMERA_MISTAKE = 40f;
    private const float MIN_ENERGY_VALUE = 5;
    private const float SPEED_BOOST = 1.8f;

    [SerializeField] private Transform _head;
    [SerializeField] private float _speed;

    private Rigidbody _rb;
    private Quaternion _startHeadRotation;
    private readonly float _energyRegenRate = 5f;
    private readonly float _energyConsumptionRate = 10f;
    private float _vertical;
    private float _horizontal;
    private float _xRotationCamera;
    private float _startSpeed;
    private bool _isRunning = false;
    private bool _isGrounded;

    public float MouseSensitivy;
    public float Energy;
    public float Hp = 100f;
    public readonly float EnergyMaxValue = 50f;
    public readonly float HpMaxValue = 100f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        _startHeadRotation = _head.rotation;
        _xRotationCamera = _head.localRotation.eulerAngles.x;
        Energy = EnergyMaxValue;
        _startSpeed = _speed;
    }
    private void OnCollisionEnter(Collision other)
    {
        _isGrounded = true;
    }

    private void FixedUpdate()
    {
        if (_isGrounded)
        {
            _vertical = Input.GetAxisRaw("Vertical");
            _horizontal = Input.GetAxisRaw("Horizontal");
        }
        _rb.MovePosition(_rb.position + _vertical * _speed * Time.fixedDeltaTime * transform.forward);
        _rb.MovePosition(_rb.position + _horizontal * _speed * Time.fixedDeltaTime * transform.right);

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            if (_isGrounded)
            {
                _isGrounded = false;
                _rb.AddForce(transform.up, ForceMode.Impulse);
                EventBus.PlayerJump.Invoke();
            }
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            float mouseX = Input.GetAxis("Mouse X") * MouseSensitivy * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivy * Time.deltaTime;

            _xRotationCamera -= mouseY;
            _xRotationCamera = Mathf.Clamp(_xRotationCamera, _startHeadRotation.y - ROTATION_CAMERA_MISTAKEY, _startHeadRotation.z + ROTATION_CAMERA_MISTAKE);
            _head.localRotation = Quaternion.Euler(_xRotationCamera, 0, 0);
            transform.Rotate(Vector3.up * mouseX);
        }

        if (Energy > MIN_ENERGY_VALUE && Input.GetKey(KeyCode.LeftShift))
        {
            Energy -= _energyConsumptionRate * Time.deltaTime;
            _speed = _startSpeed * SPEED_BOOST;
            _isRunning = true;
        }
        else if (!(Energy > MIN_ENERGY_VALUE) && _isRunning)
        {
            Energy = 0;
            _isRunning = false;
        }
        else if (Energy < EnergyMaxValue)
        {
            Energy += _energyRegenRate * Time.deltaTime;
            _speed = _startSpeed;
        }

        EventBus.PlayerMove.Invoke(_isGrounded, _isRunning && Input.GetKey(KeyCode.LeftShift));
    }
}