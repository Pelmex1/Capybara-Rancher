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
    IRobotParts _irobotspart;
    ICrystall _icrystall;

    private void Start()
    {
        ParentObject = gameObject.transform.parent.gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (ParentObject.TryGetComponent(out _irobotspart) && other.TryGetComponent(out _icrystall))
        {
            if (_irobotspart.CheckMoving == false && _icrystall != null)
            {
                Debug.Log("1 if was working");
                EventBus.AddToDict.Invoke();
                string name = _icrystall.NameCrystal;
                if (CheckDataCrusyl.ContainsKey(name) && CheckDataCrusyl[name] == 0)
                {
                    Debug.Log("2 if was working");
                    Destroy(other.gameObject);
                    CheckDataCrusyl[name]++;
                    CheckActiveCrystal();
                }
            }

        }
        else
            return;
    }

    private void CheckActiveCrystal()
    {
        int AmountCrystal = 0;
        foreach (var item in CheckDataCrusyl)
        {
            if (item.Value == 1)
                AmountCrystal++;
        }
        if (AmountCrystal == 3)
        {
            _irobotspart.WasBuilding = true;
            EventBus.WasAddingAllCrystall.Invoke();
        }

    }
    public void TransitionData()
    {
        CheckDataCrusyl = DictionaryCrystall;
    }
}
