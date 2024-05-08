using System.Collections.Generic;
using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;
using UnityEngine;
public class ActivatingPartRobot : MonoBehaviour
{
    [SerializeField] private Transform[] Points = new Transform[3];
    [SerializeField] private GameObject[] AllParts = new GameObject[3];
    [SerializeField] private GameObject WinPanel;
    private int AmountActivingParts;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out _irobotspart))
        {
            if (_irobotspart.CheckMoving == true)
            {
                Debug.Log("Work if");
                int index = other.GetComponent<IRobotParts>().IndexofPart;
                EventBus.OffMovebleObject.Invoke(index, Points[index]);
            }
        }
    }

    private void CheckWon()
    {
        for (int i = 0; i < AllParts.Length; i++)
        {
            if (AllParts[i].GetComponent<IRobotParts>().WasBuilding)
                AmountActivingParts++;
        }
        if (AmountActivingParts == 3)
        {
            Time.timeScale = 0;
            WinPanel.SetActive(true);
        }
        else return;
    }
}
