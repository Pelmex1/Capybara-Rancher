using CustomEventBus;
using UnityEngine;

public class ActivateBuildRobot : MonoBehaviour
{
    [SerializeField] private GameObject[] AllPartsObject = new GameObject[3];
    [SerializeField] private Transform[] Points = new Transform[3];
    [SerializeField] private GameObject WiningPanel;
    private int AmountPartsRobot;
    private void Awake()
    {
        EventBus.TransitionPratsData = TransitionPartsRobotData;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "movebleObject" && other.gameObject.GetComponent<IRobotParts>().CheckMoving == true)
        {
            int index = other.gameObject.GetComponent<IRobotParts>().IndexofPart;
            EventBus.OffMovebleObject.Invoke(other.gameObject.name, index, Points[index]);
            AmountPartsRobot++;
            CheckActivatePart();
        }
        else
        {
            return;
        }
    }
    private void TransitionPartsRobotData(GameObject _usingGameObject)
    {
        for(int i = 0; i < AllPartsObject.Length; i++)
        {
            if(AllPartsObject[i] == null)
                AllPartsObject[i] = _usingGameObject;
        }  
        _usingGameObject.GetComponent<IRobotParts>().AllPartsObject = AllPartsObject;
    }

    private void CheckActivatePart()
    {
        if (AmountPartsRobot == 3)
        {
            Time.timeScale = 0;
            WiningPanel.SetActive(true);
        }
        else return;
    }
}
