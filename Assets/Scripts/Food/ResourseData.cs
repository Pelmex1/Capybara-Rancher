using UnityEditor.ShaderGraph;
using UnityEngine;
public enum ResourseType
{
    food,
    capybara
}
public class ResourseData : MonoBehaviour
{

    public ResourseType type;
    public Sprite icon;
    public ColorRGBAControl colorInSlot;

    public int indexForSpawnFarm;
    public float timeGeneration; // if it is generable food
}
