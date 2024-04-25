using UnityEngine;
using CustomEventBus;

public class AddNewCrystal : MonoBehaviour
{
    private void OnEnable()
    {
        EventBus.SetNameCrystal.Invoke(gameObject.name);
    }
}
