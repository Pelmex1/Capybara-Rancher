using CapybaraRancher.EventBus;
using UnityEngine;
using CapybaraRancher.Interfaces;
using System.Collections.Generic;

public class AddToDictCrystall : MonoBehaviour
{
    // private Dictionary<string, int> DictionaaryCrystall = new();
    // private Dictionary<int, Dictionary<string, int>> GetAllCrystallData = new();
    private Queue<string> _queueForNameCrystall = new();
    private GameObject[] _allPartsObject = new GameObject[3];

    private void Awake()
    {
        EventBus.SetNameCrystal = SetCrystalName;
        EventBus.TransitonprivatePartsData = TransitionPartsRobot;
    }
    private void Start()
    {
        for (int i = 0; i < _allPartsObject.Length; i++)
        {
            _allPartsObject[i].GetComponent<ITransitionCrystallData>().TransitionData();
        }
    }
    private void SetCrystalName(string NameCrystal)
    {
        _queueForNameCrystall.Enqueue(NameCrystal);
        SortAndRemoveDuplicates();

        for (int i = 0; i < _allPartsObject.Length; i++)
        {
            if (_allPartsObject[i] != null)
            {
                ITransitionCrystallData transitionCrystallData = _allPartsObject[i].GetComponent<ITransitionCrystallData>();

                if (transitionCrystallData != null && transitionCrystallData.DictionaaryCrystall != null)
                {
                    Dictionary<string, int> DictionaaryCrystall = transitionCrystallData.DictionaaryCrystall;
                    while (_queueForNameCrystall.Count > 0)
                    {
                        string Name = _queueForNameCrystall.Dequeue();       
                        if (!DictionaaryCrystall.ContainsKey(Name))
                        {
                            if (_allPartsObject[i].GetComponent<IRobotParts>().CheckMoving == false)
                                DictionaaryCrystall.Add(Name, 0);
                            else
                                DictionaaryCrystall.Add(Name, 1);
                        }
                        else continue;
                    }
                    _allPartsObject[i].GetComponent<ITransitionCrystallData>().DictionaaryCrystall = DictionaaryCrystall;
                }
            }

        }

    }

    private void SortAndRemoveDuplicates()
    {
        List<string> tempList = new List<string>(_queueForNameCrystall);
        tempList.Sort();
        Queue<string> newqueue = new Queue<string>();
        string previousItem = "";
        foreach (string item in tempList)
        {
            if (item != previousItem)
            {
                newqueue.Enqueue(item);
                previousItem = item;
            }
        }
        _queueForNameCrystall = newqueue;
    }
    private void TransitionPartsRobot(GameObject[] AllPartsObject)
    {
        _allPartsObject = AllPartsObject;
        //Debug.Log("Work transit");
    }
}
public class GroupByComparator : IComparer<string>
{
    public int Compare(string x, string y)
    {
        int result = x.CompareTo(y);
        if (result == 0)
        {
            return 0;
        }
        return result;
    }
}


