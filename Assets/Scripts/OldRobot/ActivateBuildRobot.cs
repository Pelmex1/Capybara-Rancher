using System.Collections.Generic;
using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;
using UnityEngine;

public class ActivateBuildRobot : MonoBehaviour, ITransitionCrystallData
{
    public bool WasChangeDict { get; set; } = false;
    public Dictionary<string, int> DictionaryCrystall { get; set; } = new();
    private Dictionary<string, int> CheckDataCrusyl = new Dictionary<string, int>();
    private GameObject ParentObject;

    private void Start()
    {
        ParentObject = gameObject.transform.parent.gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "movebleObject" && ParentObject.GetComponent<IRobotParts>().CheckMoving == false)
        {
            Debug.Log("1 if was working");
            EventBus.AddToDict.Invoke();
            string name = other.GetComponent<ICrystall>().NameCrystal;
            if (CheckDataCrusyl.ContainsKey(name) && CheckDataCrusyl[name] == 0)
            {
                Debug.Log("2 if was working");
                Destroy(other.gameObject);
                CheckDataCrusyl[name]++;
                CheckActiveCrystal();
            }
            // else if (CheckDataCrusyl.ContainsKey(other.name)!)
            // {
            //     EventBus.AddToDict.Invoke();
            //     Destroy(other.gameObject);
            //     CheckDataCrusyl[other.name]++;
            //     CheckActiveCrystal();
            // }
        }
        else
            return;
    }

    private void CheckActiveCrystal()
    {
        int AmountCrystal = 0;
        Debug.Log(ParentObject.name);
        foreach (var item in CheckDataCrusyl)
        {
            Debug.Log(item.Key);
            Debug.Log(item.Value);
        }
        foreach (var item in CheckDataCrusyl)
        {
            if (item.Value == 1 & ParentObject.GetComponent<IRobotParts>().CheckMoving == false)
            {
                AmountCrystal++;
            }
        }
        Debug.Log(AmountCrystal);
        if (AmountCrystal == 3)
        {
            ParentObject.GetComponent<IRobotParts>().WasBuilding = true;
            EventBus.WasAddingAllCrystall.Invoke();
        }

    }
    public void TransitionData()
    {
        CheckDataCrusyl = DictionaryCrystall;
    }
}
