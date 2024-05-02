using UnityEngine;
using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;

public class AddPartsToArray : MonoBehaviour
{
    [SerializeField] private GameObject[] _allPartsObject = new GameObject[3];
    public GameObject[] PartsObject { get; set; }

    private void OnEnable()
    {
        EventBus.TransitionPratsData = TransitionPartsRobotData;
        EventBus.TransitonprivatePartsData.Invoke(_allPartsObject);

    }
    private void OnDisable()
    {
        EventBus.TransitionPratsData = TransitionPartsRobotData;
        EventBus.TransitonprivatePartsData.Invoke(_allPartsObject);
    }
    private void TransitionPartsRobotData(GameObject _usingGameObject)
    {
        for (int i = 0; i < _allPartsObject.Length; i++)
        {
            if (_allPartsObject[i] == null)
            {
                _allPartsObject[i] = _usingGameObject;
                break;
            }              
        }
        _usingGameObject.GetComponent<IRobotParts>().AllPartsObject = _allPartsObject;
    }
}
