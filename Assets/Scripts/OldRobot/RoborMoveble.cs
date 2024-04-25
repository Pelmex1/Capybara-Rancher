using CustomEventBus;
using UnityEngine;

public class RoborMoveble : MovebleObject, IRobotParts
{
    public int IndexofPart { get; set; }
    public bool CheckMoving { get; set; }
    [SerializeField] private int _index;
    public GameObject[] AllPartsObject { get; set; }
    private Transform stateTransform;
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
    }


    private void OnObject(string NameObject, int OnIndexofPart)
    {
        for (int i = 0; i < AllPartsObject.Length; i++)
        {
            if (i == OnIndexofPart & AllPartsObject[OnIndexofPart].gameObject.name == NameObject)
            {
                AllPartsObject[OnIndexofPart].gameObject.GetComponent<IRobotParts>().CheckMoving = true;
            }
        }
    }
    private void OffObject(string NameObject, int OffIndexofPart, Transform PointTransform)
    {
        stateTransform = PointTransform;
        for (int i = 0; i < AllPartsObject.Length; i++)
        {
            if (i == OffIndexofPart & AllPartsObject[OffIndexofPart].gameObject.name == NameObject)
            {
                AllPartsObject[OffIndexofPart].gameObject.GetComponent<IRobotParts>().CheckMoving = false;
                AllPartsObject[OffIndexofPart].gameObject.tag = "PartsRobot";
                AllPartsObject[OffIndexofPart].gameObject.transform.position = stateTransform.position;
            }
        }
        // if (AllPartsObject[OffIndexofPart].gameObject.name == NameObject)
        // {
        //     gameObject.tag = "PartsRobot";
        //     gameObject.transform.position = stateTransform.position;
        //     gameObject.transform.rotation = stateTransform.rotation;
        //     gameObject.transform.localScale = stateTransform.localScale;
        // }
    }

}
