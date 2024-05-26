using UnityEngine;
using CapybaraRancher.EventBus;

public class SavesRemove : MonoBehaviour
{
    [SerializeField] private bool _savesDelete;
    [SerializeField] private bool _addMoney;

    private void Start()
    {
        if (_savesDelete)
            PlayerPrefs.DeleteAll();
        if (_addMoney)
            EventBus.AddMoney(1000f);
    }

    private void OnEnable()
    {
        if (_savesDelete)
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs deleted");
        }
    }
}
