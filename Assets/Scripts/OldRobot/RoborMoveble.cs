using CapybaraRancher.EventBus;
using UnityEngine;
using CapybaraRancher.Interfaces;
using UnityEngine.UIElements;

public class RoborMoveble : MovebleObject, IRobotParts
{
    public int IndexofPart { get; set; }
    public bool CheckMoving { get; set; }
    public bool WasBuilding { get; set; } = false;
    public GameObject[] AllPartsObject { get; set; }
    [SerializeField] private int _index;
    private void Awake()
    {
        // PlayerPrefs.DeleteAll();
        EventBus.OnMovebleObject = OnObject;
        EventBus.OffMovebleObject = OffObject;
        IndexofPart = _index;
    }
    protected override void Update()
    {
        if (CheckMoving)
        {
            base.Update();
        }
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
    }

    protected override void OnDisable()
    {
        // Debug.Log(CheckMoving);
        if (CheckMoving)
            PlayerPrefs.SetInt($"CanMoving{_index}", 0);
        else
            PlayerPrefs.SetInt($"CanMoving{_index}", 1);
        PlayerPrefs.Save();
        base.OnDisable();
        Debug.Log(CheckMoving + $" {gameObject.name}");
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (CheckMoving)
        {
            base.OnTriggerEnter(other);
        }
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
                _usingObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                _usingObject.transform.localScale = new Vector3(100f, 100f, 30f);
            }
        }
    }
}
