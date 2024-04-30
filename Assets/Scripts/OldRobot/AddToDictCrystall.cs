using System.Collections.Generic;
using CapybaraRancher.EventBus;
using UnityEngine;

public class AddToDictCrystall : MonoBehaviour
{
    public Dictionary<string, int> DictionaaryCrystall = new Dictionary<string, int>();
    private Queue<string> _queueForNameCrystall = new Queue<string>();

    private void Awake()
    {
        EventBus.SetNameCrystal = SetCrystalName;
    }
    private void Start()
    {
        EventBus.TranstionCrystallData.Invoke(DictionaaryCrystall);
    }
    private void SetCrystalName(string NameCrystal)
    {
        _queueForNameCrystall.Enqueue(NameCrystal);
        SortAndRemoveDuplicates();
        while (_queueForNameCrystall.Count > 0)
        {
            string Name = _queueForNameCrystall.Dequeue();
            if (!DictionaaryCrystall.ContainsKey(Name))
                DictionaaryCrystall.Add(Name, 0);
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


