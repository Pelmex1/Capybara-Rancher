using System.Collections.Generic;
using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;
using UnityEngine;

public class ActivatingPartRobot : MonoBehaviour, ITransitionCrystallData
{
    public Dictionary<string, int> DictionaaryCrystall {get; set;} = new();
    private Dictionary<string, int> CheckDataCrusyl = new Dictionary<string, int>();
    private int AmountCrystal;
    private List<string> UsedParts = new List<string>();

    // private void Awake()
    // {
    //     DictionaaryCrystall = CheckDataCrusyl;
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "movebleObject")
        {
            if (CheckDataCrusyl.ContainsKey(other.name) && CheckDataCrusyl[other.name] == 0)
            {
                Destroy(other.gameObject);
                CheckDataCrusyl[other.name]++;
                CheckActiveCrystal();
            }
        }
        else
            return;
    }

    private void CheckActiveCrystal()
    {
        foreach (var item in CheckDataCrusyl)
        {
            if (item.Value == 1 & AmountCrystal != 3 & UsedParts.Contains(item.Key)!)
            {
                AmountCrystal++;
            }
            UsedParts.Add(item.Key);
        }
        // Debug.Log(AmountCrystal);
        if (AmountCrystal == 3)
            gameObject.tag = "movebleObject";
            EventBus.OnMovebleObject.Invoke(gameObject.name, gameObject.GetComponent<IRobotParts>().IndexofPart);
    }
    public void TransitionData()
    {
        CheckDataCrusyl = DictionaaryCrystall;
        Debug.Log(CheckDataCrusyl);
    }
}
