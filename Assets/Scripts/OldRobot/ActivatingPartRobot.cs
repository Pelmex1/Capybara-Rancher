using System.Collections.Generic;
using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;
using UnityEngine;
using UnityEngine.UIElements;

public class ActivatingPartRobot : MonoBehaviour, ITransitionCrystallData
{
    public bool WasChangeDict { get; set; } = false;
    public Dictionary<string, int> DictionaryCrystall { get; set; } = new();
    private Dictionary<string, int> CheckDataCrusyl = new Dictionary<string, int>();
    private int AmountCrystal;
    private List<string> UsedParts = new List<string>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "movebleObject")
        {
            EventBus.AddToDict.Invoke();
            if (CheckDataCrusyl.ContainsKey(other.name) && CheckDataCrusyl[other.name] == 0)
            {
                Destroy(other.gameObject);
                CheckDataCrusyl[other.name]++;
                CheckActiveCrystal();
            }
            else if (CheckDataCrusyl.ContainsKey(other.name)!)
            {
                EventBus.AddToDict.Invoke();
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
        if (AmountCrystal == 3)
            gameObject.tag = "movebleObject";
        EventBus.OnMovebleObject.Invoke(gameObject.name, gameObject.GetComponent<IRobotParts>().IndexofPart);
    }
    public void TransitionData()
    {
        CheckDataCrusyl = DictionaryCrystall;
    }
}
