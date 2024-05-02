using CapybaraRancher.EventBus;
using UnityEngine;
using CapybaraRancher.Interfaces;

public class RoborMoveble : MovebleObject, IRobotParts
{
    public int IndexofPart { get; set; }
    public bool CheckMoving { get; set; }
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
            CheckMoving = false;
        else
        {
            CheckMoving = true;
            gameObject.tag = "movebleObject";
        }
        Debug.Log(CheckMoving + $" {gameObject.name}");
        EventBus.TransitionPratsData.Invoke(gameObject);
    }

    private void OnDisable()
    {
        // Debug.Log(CheckMoving);
        if (CheckMoving)
            PlayerPrefs.SetInt($"CanMoving{_index}", 1);
        else
            PlayerPrefs.SetInt($"CanMoving{_index}", 0);
        PlayerPrefs.Save();
        Debug.Log(CheckMoving + $" {gameObject.name}");
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
    private void OffObject(string NameObject, int OffIndexofPart, Transform PointTransform)
    {
        for (int i = 0; i < AllPartsObject.Length; i++)
        {
            if (i == OffIndexofPart & AllPartsObject[OffIndexofPart].gameObject.name == NameObject)
            {
                AllPartsObject[OffIndexofPart].GetComponent<IRobotParts>().CheckMoving = false;
                AllPartsObject[OffIndexofPart].tag = "PartsRobot";
                AllPartsObject[OffIndexofPart].transform.SetParent(PointTransform);
                AllPartsObject[OffIndexofPart].transform.localPosition = new Vector3(0f,0f,0f);
            }
        }
    }

}
