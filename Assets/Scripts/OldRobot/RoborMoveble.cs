using CapybaraRancher.EventBus;
using UnityEngine;
using CapybaraRancher.Interfaces;
using System.Collections;
public class RoborMoveble : MonoBehaviour, IRobotParts, IMovebleObject
{
    public int IndexofPart { get; set; }
    public bool CheckMoving { get; set; }
    public bool WasBuilding { get; set; } = false;
    public GameObject[] AllPartsObject { get; set; }
    public InventoryItem Data { get => inventoryItem; set => inventoryItem = value; }
    public GameObject Localgameobject { get => gameObject; set { return; } }
    public bool IsMoved { get; set; } = false;
    [SerializeField] private int _index;
    [SerializeField] private InventoryItem inventoryItem;
    private const string CANON_TAG = "CanonEnter";
    private bool _looted = false;
    private bool _isDisabled = false;
    private IObjectSpawner _objectSpawner;


    private void Awake()
    {
        // PlayerPrefs.DeleteAll();
        EventBus.OnMovebleObject = OnObject;
        EventBus.OffMovebleObject = OffObject;
        IndexofPart = _index;
    }
    private void OnEnable()
    {

        if (PlayerPrefs.GetInt($"CanMoving{_index}") == 0)
        {
            CheckMoving = true;
            gameObject.tag = "movebleObject";
            WasBuilding = false;
        }
        else
        {
            CheckMoving = false;
            WasBuilding = true;
            gameObject.tag = "PartsRobot";
        }
        Debug.Log(CheckMoving + $" {gameObject.name}");
        _isDisabled = true;
        StartCoroutine(Disabled());
    }

    private void OnDisable()
    {
        // Debug.Log(CheckMoving);
        if (CheckMoving)
            PlayerPrefs.SetInt($"CanMoving{_index}", 0);
        else
            PlayerPrefs.SetInt($"CanMoving{_index}", 1);
        PlayerPrefs.Save();
        _looted = false;
        Debug.Log(CheckMoving + $" {gameObject.name}");
    }
    private void Start()
    {
        transform.parent?.TryGetComponent(out _objectSpawner);
    }
    private void Update()
    {
        if (CheckMoving)
        {
            if (Input.GetMouseButton(0) && Time.timeScale > 0)
            {
                if (EventBus.CheckList(gameObject)) IsMoved = true;
            }
            else IsMoved = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (CheckMoving)
        {
            if (other.CompareTag(CANON_TAG) && !_looted && !_isDisabled)
            {
                _looted = true;
                if (EventBus.AddItemInInventory(Data) == true)
                {
                    EventBus.AddInPool(gameObject, Data.TypeGameObject);
                    EventBus.RemoveFromList(gameObject);
                    ItemActivator.ActivatorItemsRemove(gameObject);
                    _objectSpawner?.ReturnToPool(gameObject);
                    gameObject.SetActive(false);
                }
            }
        }
    }
    private IEnumerator Disabled()
    {
        yield return new WaitForSecondsRealtime(2);
        _isDisabled = false;
    }
    private void OnObject(string NameObject, int OnIndexofPart)
    {
        for (int i = 0; i < AllPartsObject.Length; i++)
        {
            if (i == OnIndexofPart & AllPartsObject[OnIndexofPart].name == NameObject)
            {
                AllPartsObject[OnIndexofPart].GetComponent<IRobotParts>().CheckMoving = true;
            }
        }
    }
    private void OffObject(int OffIndexofPart, Transform PointTransform)
    {
        for (int i = 0; i < AllPartsObject.Length; i++)
        {
            if (i == OffIndexofPart)
            {
                GameObject _usingObject = AllPartsObject[OffIndexofPart];
                _usingObject.GetComponent<IRobotParts>().CheckMoving = false;
                _usingObject.tag = "PartsRobot";
                _usingObject.transform.SetParent(PointTransform);
                _usingObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                _usingObject.transform.localPosition = new Vector3(0f, 0f, 0f);
                if(_usingObject != AllPartsObject[2])
                    _usingObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                else 
                    _usingObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                _usingObject.transform.localScale = new Vector3(100f, 100f, 30f);
            }
        }
    }
}
