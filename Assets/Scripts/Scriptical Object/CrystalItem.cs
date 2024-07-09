using UnityEngine;
using CapybaraRancher.Enums;
using CapybaraRancher.Interfaces;

public class CrystalItem : MonoBehaviour, ICrystalItem
{
    [SerializeField] private CapybaraData typeData;
    [SerializeField] private float price;
    [SerializeField] private float percentOfRegen;
    public CapybaraData TypeData
    {
        get { return typeData; }
        set { typeData = value; }
    }

    public float Price
    {
        get { return price; }
        set { price = value; }
    }

    public float PercentOfRegen
    {
        get { return percentOfRegen; }
        set { percentOfRegen = value; }
    }
}
