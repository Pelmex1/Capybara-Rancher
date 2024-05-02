using CapybaraRancher.EventBus;
using UnityEngine;
using CapybaraRancher.Interfaces;
using System.Collections.Generic;
public class AddToDictCrystall : MonoBehaviour
{
    private Queue<string> _queueForNameCrystall = new();
    private GameObject[] _allPartsObject = new GameObject[3];

    private void Awake()
    {
        EventBus.SetNameCrystal = SetCrystalName;
        EventBus.TransitonprivatePartsData = TransitionPartsRobot;
        EventBus.AddToDict = AddToDict;
    }
    private void SetCrystalName(string NameCrystal)
    {
        _queueForNameCrystall.Enqueue(NameCrystal);
    }

    private void AddToDict()
    {
        for (int i = 0; i < _allPartsObject.Length; i++)
        {
            if (_allPartsObject[i].GetComponent<ITransitionCrystallData>().WasChangeDict == false)
            {
                Queue<string> TransitionQueue = new Queue<string>(_queueForNameCrystall);
                Dictionary<string, int> DictionaryCrystall = _allPartsObject[i].GetComponent<ITransitionCrystallData>().DictionaryCrystall;
                while (TransitionQueue.Count > 0)
                {
                    string Name = TransitionQueue.Dequeue();
                    if (!DictionaryCrystall.ContainsKey(Name))
                    {
                        if (_allPartsObject[i].GetComponent<IRobotParts>().CheckMoving == false)
                            DictionaryCrystall.Add(Name, 0);
                        else
                            DictionaryCrystall.Add(Name, 1);
                    }
                }
                _allPartsObject[i].GetComponent<ITransitionCrystallData>().DictionaryCrystall = DictionaryCrystall;
                _allPartsObject[i].GetComponent<ITransitionCrystallData>().WasChangeDict = true;
                _allPartsObject[i].GetComponent<ITransitionCrystallData>().TransitionData();
            }
        }
    }
    private void TransitionPartsRobot(GameObject[] AllPartsObject)
    {
        _allPartsObject = AllPartsObject;
    }
}