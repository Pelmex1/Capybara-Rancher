using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Items> items = new List<Items>();
    public void LoadData(Items obj)
    {
        items.Add(obj);
    }
}
