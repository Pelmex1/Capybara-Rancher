using UnityEngine;
using CapybaraRancher.EventBus;

public class AddNewCrystal : MonoBehaviour
{
    private void OnEnable()
    {
        EventBus.SetNameCrystal.Invoke(gameObject.name);
    }
}
