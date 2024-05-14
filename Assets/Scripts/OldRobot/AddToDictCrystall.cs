using CapybaraRancher.EventBus;
using UnityEngine;
using CapybaraRancher.Interfaces;
using System.Collections.Generic;
using DevionGames;
public class AddToDictCrystall : MonoBehaviour
{
    private Queue<string> _queueForNameCrystall = new();
    private GameObject[] _allChildrenPartsObject = new GameObject[3];

    private void Awake()
    {
        EventBus.SetNameCrystal = SetCrystalName;
        EventBus.TransitonPartsData = TransitionPartsRobot;
        EventBus.AddToDict = AddToDict;
    }
    private void SetCrystalName(string NameCrystal)
    {
        _queueForNameCrystall.Enqueue(NameCrystal);
    }

    private void AddToDict()
    {
        for (int i = 0; i < _allChildrenPartsObject.Length; i++)
        {
            if (_allChildrenPartsObject[i].GetComponent<ITransitionCrystallData>().WasChangeDict == false)
            {
                Queue<string> TransitionQueue = new Queue<string>(_queueForNameCrystall);
                Dictionary<string, int> DictionaryCrystall = _allChildrenPartsObject[i].GetComponent<ITransitionCrystallData>().DictionaryCrystall;
                while (TransitionQueue.Count > 0)
                {
                    string Name = TransitionQueue.Dequeue();
                    if (!DictionaryCrystall.ContainsKey(Name))
                        DictionaryCrystall.Add(Name, 0);
                }
                _allChildrenPartsObject[i].GetComponent<ITransitionCrystallData>().DictionaryCrystall = DictionaryCrystall;
                _allChildrenPartsObject[i].GetComponent<ITransitionCrystallData>().WasChangeDict = true;
            }
        }
    }
    private void TransitionPartsRobot(GameObject[] AllPartsObject)
    {
        for (int i = 0; i < AllPartsObject.Length; i++)
        {
            _allChildrenPartsObject[i] = AllPartsObject[i].FindChild("Collider", true);
        }
    }
}