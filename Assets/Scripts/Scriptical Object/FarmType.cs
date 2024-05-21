using CapybaraRancher.Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "FarmType", menuName = "Capybara-Rancher/FarmType", order = 0)]
public class FarmType : ScriptableObject 
{
    public FoodType type;
    public GameObject farm;
}