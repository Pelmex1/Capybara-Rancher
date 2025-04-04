using CapybaraRancher.Consts;
using CapybaraRancher.Enums;
using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;
using System.Collections;
using UnityEngine;

public class MovingPlayer : MonoBehaviour, IPlayer
{


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
    private bool _runCalled = false;
    private bool _isGrounded;
    private bool _isBuyJump;
    private bool _isActivatedJump = false;

    public float MouseSensitivy;
    public float Energy { get; set; }
    public float Health { get; set; }
    public float Hunger { get; set; }
    public float EnergyMaxValue { get; set; }
    public float HealthMaxValue { get; set; }
    public float HungerMaxValue { get; set; }

    private void Awake()
    {
        EventBus.BuyJump = (bool isBuy) => _isBuyJump = isBuy;
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
        _isBuyJump = PlayerPrefs.GetString("SatchelBuy","false") == "true";
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

        _runCalled = false;
    }
    private void Jump(){
        if(_isBuyJump && _isActivatedJump && Energy >= 10)
        {
            _rb.AddForce(transform.up / Constants.SPEED_SATCHEL, ForceMode.Impulse);
            Energy -= 0.3f;
        } else
        if (_isGrounded)
        {
            _isGrounded = false;
            _rb.AddForce(transform.up, ForceMode.Impulse);
            EventBus.PlayerJump.Invoke();
            EventBus.MovingTutorial.Invoke();
        }
    }
    private void Run(){
        if (Energy > Constants.MIN_ENERGY_VALUE)
        {
            Energy -= _energyConsumptionRate * Time.deltaTime;
            _speed = _startSpeed * Constants.SPEED_BOOST;
            _isRunning = true;
        }
        else if (!(Energy > Constants.MIN_ENERGY_VALUE))
        {
            Energy = 0;
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
            _xRotationCamera = Mathf.Clamp(_xRotationCamera, _startHeadRotation.y - Constants.ROTATION_CAMERA_MISTAKE_Y, _startHeadRotation.z + Constants.ROTATION_CAMERA_MISTAKE_Z);
            _head.localRotation = Quaternion.Euler(_xRotationCamera, 0, 0);
            transform.Rotate(Vector3.up * mouseX);
        }

        EventBus.PlayerMove.Invoke(_isGrounded && (_vertical != 0 || _horizontal != 0), _isRunning);

        EventBus.GiveEnergyPlayerData.Invoke(Health, Energy, Hunger);
        if (Health <= 0)
            StartCoroutine(Respawn(gameObject));
    }
    private void LateUpdate()
    {
        if (Energy < EnergyMaxValue && !_runCalled)
        {
            Energy += _energyRegenRate * Time.deltaTime;
            _speed = _startSpeed;
            _runCalled = false;
        }
    }
    private void WasChangeSenstive(float ValueSensative) => MouseSensitivy = ValueSensative;
    private void EnableSatchel() => _isActivatedJump = !_isActivatedJump;

    private IEnumerator Respawn(GameObject player)
    {
        EventBus.PlayerRespawned.Invoke();

        yield return new WaitForSeconds(_respawnDelay);

        player.transform.SetPositionAndRotation(_spawnTransform.position, _spawnTransform.rotation);
    }
    private void FullStats()
    {
        Energy = EnergyMaxValue;
        Health = HealthMaxValue;
        Hunger = HungerMaxValue;
    }
    private void SetStats()
    {
        EnergyMaxValue = PlayerPrefs.GetFloat("EnergyMaxValue",Constants.DEFAULT_ENERGY_MAXVALUE);
        PlayerPrefs.SetFloat("EnergyMaxValue", EnergyMaxValue);
        HealthMaxValue = PlayerPrefs.GetFloat("HealthMaxValue", Constants.DEFAULT_HEALTH_MAXVALUE);
        PlayerPrefs.SetFloat("HealthMaxValue", HealthMaxValue);
        HungerMaxValue = PlayerPrefs.GetFloat("HungerMaxValue", Constants.DEFAULT_HUNGER_MAXVALUE);
        PlayerPrefs.SetFloat("HungerMaxValue", HungerMaxValue);
    }

    private void SetEnergySpending() => _energyConsumptionRate = PlayerPrefs.GetFloat("EnergySpendingRate", Constants.DEFAULT_ENERGY_SPENDING);
    private void OnRunInput()
    {
        _runCalled = true;
    }
    private void OnEnable()
    {
        EventBus.SatchelInput += EnableSatchel;
        EventBus.JumpInput += Jump;
        EventBus.RunInput += Run;
        EventBus.RunInput += OnRunInput;
        EventBus.PlayerRespawned += FullStats;
        EventBus.WasChangeMouseSensetive += WasChangeSenstive;
        EventBus.MaxValueUpgrade += SetStats;
        EventBus.EnergySpendingUpgrade += SetEnergySpending;
    }

    private void OnDisable()
    {
        EventBus.SatchelInput -= EnableSatchel;
        EventBus.JumpInput -= Jump;
        EventBus.RunInput -= Run;
        EventBus.RunInput -= OnRunInput;
        EventBus.WasChangeMouseSensetive -= WasChangeSenstive;
        EventBus.MaxValueUpgrade -= SetStats;
        EventBus.PlayerRespawned -= FullStats;
        EventBus.EnergySpendingUpgrade -= SetEnergySpending;
    }
}