using CapybaraRancher.EventBus;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    [SerializeField] private FarmType[] farms;
    private void Awake() {
        EventBus.GetFarms = () => 
        {
            return farms;
        };
    }
}
