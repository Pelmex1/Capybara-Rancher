using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;
using UnityEngine;

public class SavePosition : MonoBehaviour
{
    private bool _isOnAplicationQuit = false;
    private void OnEnable() {
        try
        {
            EventBus.RemoveFromList(gameObject);
        } finally {} // Он не может существовать один
    }
    private void Start() {
        if(gameObject.CompareTag("Player"))
        {
            Vector3 _positionNow = transform.position;
            float x = PlayerPrefs.GetFloat("Player_X",_positionNow.x);
            float y = PlayerPrefs.GetFloat("Player_Y",_positionNow.y);
            float z = PlayerPrefs.GetFloat("Player_Z",_positionNow.z);
            transform.position = new(x,y,z);
        } else {
            if(PlayerPrefs.GetString($"{transform.parent?.name}_{name}_isEnable", "true") == "false")
            {   
                EventBus.AddInPool(gameObject, GetComponent<IMovebleObject>().Data.TypeGameObject);
                gameObject.SetActive(false);
            } else {
                Vector3 _positionNow = transform.position;
                float x = PlayerPrefs.GetFloat($"{transform.parent?.name}_{name}_X",_positionNow.x);
                float y = PlayerPrefs.GetFloat($"{transform.parent?.name}_{name}_Y",_positionNow.y);
                float z = PlayerPrefs.GetFloat($"{transform.parent?.name}_{name}_Z",_positionNow.z);           
                Vector3 localPosition = new(x,y,z);
                transform.position = transform.position == localPosition ? transform.position : localPosition;
            }
        }
    }
    private void OnDisable() {
        if(!_isOnAplicationQuit)
        EventBus.AddInDisable(gameObject);
    }
    private void OnApplicationQuit() {
        _isOnAplicationQuit = true;
        if(gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetFloat("Player_X", gameObject.transform.position.x);
            PlayerPrefs.SetFloat("Player_Y", gameObject.transform.position.y);
            PlayerPrefs.SetFloat("Player_Z", gameObject.transform.position.z);
        } else {
            Vector3 _positionNow = transform.position;
            PlayerPrefs.SetFloat($"{transform.parent?.name}_{name}_X",_positionNow.x);
            PlayerPrefs.SetFloat($"{transform.parent?.name}_{name}_Y",_positionNow.y);
            PlayerPrefs.SetFloat($"{transform.parent?.name}_{name}_Z",_positionNow.z);
            PlayerPrefs.SetString($"{transform.parent?.name}_{name}_isEnable", "true");
        }
    }
}
