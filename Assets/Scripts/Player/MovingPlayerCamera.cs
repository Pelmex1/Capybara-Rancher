using CapybaraRancher.Enums;
using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;
using System.Collections;
using UnityEngine;

public class MovingPlayer : MonoBehaviour, IPlayer
{
    private const float ROTATION_CAMERA_MISTAKE_Y = 85f;
    private const float ROTATION_CAMERA_MISTAKE_Z = 40f;
    private const float MIN_ENERGY_VALUE = 5;
    private const float SPEED_BOOST = 1.8f;
    private const float DEFAULT_ENERGY_MAXVALUE = 50f;
    private const float DEFAULT_HEALTH_MAXVALUE = 100f;
    private const float DEFAULT_HUNGER_MAXVALUE = 100f;
    private const float DEFAULT_ENERGY_SPENDING = 10f;

    [SerializeField] private Transform _head;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _spawnTransform;
    [SerializeField] private float _respawnDelay = 0.5f;

    private Rigidbody _rb;
    private Quaternion _startHeadRotation;
    private readonly float _energyRegenRate = 5f;
    private float _energyConsumptionRate;
    private float _vertical;
    private float _horizontal;
    private float _xRotationCamera;
    private float _startSpeed;
    private bool _isRunning = false;
    private bool _isGrounded;

    public float MouseSensitivy;
    public float Energy { get; set; }
    public float Health { get; set; }
    public float Hunger { get; set; }
    public float EnergyMaxValue { get; set; }
    public float HealthMaxValue { get; set; }
    public float HungerMaxValue { get; set; }

    private void Awake()
    {
        SetStats();
        SetEnergySpending();
    }

    private void Start()
    {
        EventBus.GetEnergyPlayerData();
        _rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        _startHeadRotation = _head.rotation;
        _xRotationCamera = _head.localRotation.eulerAngles.x;
        _startSpeed = _speed;
        FullStats();
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
    private void Jump(){
        if (_isGrounded)
        {
            _isGrounded = false;
            _rb.AddForce(transform.up, ForceMode.Impulse);
            EventBus.PlayerJump.Invoke();
            EventBus.MovingTutorial.Invoke();
        }
    }
    private void Run(){
        if (Energy > MIN_ENERGY_VALUE)
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
            _isRunning = false;
        }
    }

    private void Update()
    {
        EventBus.GiveEnergyPlayerData.Invoke(Health, Energy, Hunger);
        
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            float mouseX = Input.GetAxis("Mouse X") * MouseSensitivy * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivy * Time.deltaTime;

            _xRotationCamera -= mouseY;
            _xRotationCamera = Mathf.Clamp(_xRotationCamera, _startHeadRotation.y - ROTATION_CAMERA_MISTAKE_Y, _startHeadRotation.z + ROTATION_CAMERA_MISTAKE_Z);
            _head.localRotation = Quaternion.Euler(_xRotationCamera, 0, 0);
            transform.Rotate(Vector3.up * mouseX);
        }


        EventBus.PlayerMove.Invoke(_isGrounded && (_vertical != 0 || _horizontal != 0), _isRunning);

        EventBus.GiveEnergyPlayerData.Invoke(Health, Energy, Hunger);
        if (Health <= 0)
            StartCoroutine(Respawn(gameObject));
    }

    private void OnEnable()
    {
        EventBus.JumpInput += Jump;
        EventBus.RunInput += Run;
        EventBus.PlayerRespawned += FullStats;
        EventBus.WasChangeMouseSensetive += WasChangeSenstive;
        EventBus.MaxValueUpgrade += SetStats;
        EventBus.EnergySpendingUpgrade += SetEnergySpending;
    }

    private void OnDisable()
    {
        EventBus.JumpInput -= Jump;
        EventBus.RunInput -= Run;
        EventBus.WasChangeMouseSensetive -= WasChangeSenstive;
        EventBus.MaxValueUpgrade -= SetStats;
        EventBus.PlayerRespawned -= FullStats;
        EventBus.EnergySpendingUpgrade -= SetEnergySpending;
    }

    private void WasChangeSenstive(float ValueSensative) => MouseSensitivy = ValueSensative;

    private IEnumerator Respawn(GameObject player)
    {
        EventBus.PlayerRespawned.Invoke();

        yield return new WaitForSeconds(_respawnDelay);

        player.transform.position = _spawnTransform.position;
        player.transform.rotation = _spawnTransform.rotation;
    }
    private void FullStats()
    {
        Energy = EnergyMaxValue;
        Health = HealthMaxValue;
        Hunger = HungerMaxValue;
    }

    private void SetStats()
    {
        EnergyMaxValue = PlayerPrefs.GetFloat("EnergyMaxValue", DEFAULT_ENERGY_MAXVALUE);
        PlayerPrefs.SetFloat("EnergyMaxValue", EnergyMaxValue);
        HealthMaxValue = PlayerPrefs.GetFloat("HealthMaxValue", DEFAULT_HEALTH_MAXVALUE);
        PlayerPrefs.SetFloat("HealthMaxValue", HealthMaxValue);
        HungerMaxValue = PlayerPrefs.GetFloat("HungerMaxValue", DEFAULT_HUNGER_MAXVALUE);
        PlayerPrefs.SetFloat("HungerMaxValue", HungerMaxValue);
    }

    private void SetEnergySpending()
    {
        _energyConsumptionRate = PlayerPrefs.GetFloat("EnergySpendingRate", DEFAULT_ENERGY_SPENDING);
    }
}