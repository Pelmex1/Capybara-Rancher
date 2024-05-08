using UnityEngine;
using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;

public class AddNewCrystal : MonoBehaviour , ICrystall
{
    public string NameCrystal {get; set;}
    [SerializeField] private string _nameCrystal;
    private void OnEnable()
    {
        NameCrystal = _nameCrystal;
        EventBus.SetNameCrystal.Invoke(_nameCrystal);
    }
}
