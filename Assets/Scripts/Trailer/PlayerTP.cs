using UnityEngine;

public class PlayerTP : MonoBehaviour
{
    [SerializeField] private Transform _tpTarget;

    public void TP()
    {
        gameObject.transform.position = _tpTarget.position;
        gameObject.transform.rotation = _tpTarget.rotation;
    }
}
