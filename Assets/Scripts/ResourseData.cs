using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;

public class ResourseData : MonoBehaviour
{
    public enum ResourseType
    {
        food,
        capybara
    }

    public ResourseType type;
    public Image icon;
    public ColorRGBAControl colorInSlot;

    public float timeGeneration; // if it is generable food
}
