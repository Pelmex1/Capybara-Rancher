using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;
using UnityEngine;

public class ActivateBuildRobot : MonoBehaviour
{
    [SerializeField] private Transform[] Points = new Transform[3];
    [SerializeField] private GameObject WiningPanel;
    private int AmountPartsRobot;
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
