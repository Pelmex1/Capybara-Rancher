using UnityEngine;

[CreateAssetMenu(fileName = "FarmObject", menuName = "FarmObject", order = 0)]
public class FarmObject : ScriptableObject {
    public GameObject Prefab;
    public int Price;
    public static explicit operator (GameObject, int)(FarmObject counter)
    {
            return new (counter.Prefab,counter.Price);
    }
}