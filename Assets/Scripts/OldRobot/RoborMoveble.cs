using CapybaraRancher.EventBus;
using UnityEngine;
using CapybaraRancher.Interfaces;

public class RoborMoveble : MovebleObject, IRobotParts
{
    public int IndexofPart { get; set; }
    public bool CheckMoving { get; set; }
    public bool WasBuilding { get; set; } = false;
    public GameObject[] AllPartsObject { get; set; }
    public bool IsMoved { get; set; } = false;
    [SerializeField] GameObject _crystallpanel;
    [SerializeField] private int _index;
    private string _saveName = "";
    private void Awake()
    {
        EventBus.OffMovebleObject = OffObject;
        _saveName = EventBus.GetSaveName();
        IndexofPart = _index;
    }
    protected override void OnEnable()
    {
        if (PlayerPrefs.GetInt($"{_saveName}CanMoving{_index}") == 0)
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
        _isDisabled = true;
        StartCoroutine(Disabled());
    }

    protected override void OnDisable()
    {
        if (CheckMoving)
            PlayerPrefs.SetInt($"{_saveName}CanMoving{_index}", 0);
        else
            PlayerPrefs.SetInt($"CanMoving{_index}", 1);
        PlayerPrefs.Save();
        _looted = false;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (CheckMoving == true)
        {
            if (other.CompareTag(CANON_TAG) && !_looted && !_isDisabled)
            {
                _looted = true;
                if (EventBus.AddItemInInventory(Data) == true)
                {
                    EventBus.RemoveFromList(gameObject);
                    gameObject.SetActive(false);
                }
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
                IRobotParts robotParts = _usingObject.GetComponent<IRobotParts>();
                robotParts.CheckMoving = false;
                robotParts.OnUI();
                _usingObject.tag = "PartsRobot";
                _usingObject.transform.SetParent(PointTransform);
                _usingObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                _usingObject.transform.SetLocalPositionAndRotation(new(0f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f));
                _usingObject.transform.localScale = new(100f, 100f, 30f);
            }
        }
    }
    public void OnUI()
    {
        _crystallpanel.SetActive(true);
    }
}
