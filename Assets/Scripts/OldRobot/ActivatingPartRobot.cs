using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;
using UnityEngine;
public class ActivatingPartRobot : MonoBehaviour
{
    [SerializeField] private Transform[] Points = new Transform[3];
    [SerializeField] private GameObject[] AllParts = new GameObject[3];
    [SerializeField] private GameObject WinPanel;
    IRobotParts _irobotspart;
    private void Awake()
    {
        EventBus.WasAddingAllCrystall = CheckWon;
        EventBus.TransitonPartsData.Invoke(AllParts);
        foreach (GameObject i in AllParts)
        {
            i.GetComponent<IRobotParts>().AllPartsObject = AllParts;
        }
    }
    private void Start()
    {
        for (int i = 0; i < AllParts.Length; i++)
        {
            if (AllParts[i].GetComponent<IRobotParts>().CheckMoving == false)
            {
                int index = AllParts[i].GetComponent<IRobotParts>().IndexofPart;
                EventBus.OffMovebleObject.Invoke(index, Points[index]);      
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out _irobotspart))
        {
            if (_irobotspart.CheckMoving == true)
            {
                int index = other.GetComponent<IRobotParts>().IndexofPart;
                EventBus.RemoveFromList.Invoke(other.gameObject);
                EventBus.OffMovebleObject.Invoke(index, Points[index]);
            }
        }
    }

    private void CheckWon()
    {
        int AmountActivingParts = 0;
        for (int i = 0; i < AllParts.Length; i++)
        {
            if (AllParts[i].GetComponent<IRobotParts>().WasBuilding)
                AmountActivingParts++;
        }
        if (AmountActivingParts == 3)
        {
            EventBus.Win.Invoke();
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;
            WinPanel.SetActive(true);
        }
        else return;
    }
}
