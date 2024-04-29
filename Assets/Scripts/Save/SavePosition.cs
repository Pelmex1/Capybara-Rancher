using UnityEngine;

public class SavePosition : MonoBehaviour
{
    private void Start() {
        if(gameObject.CompareTag("Player"))
        {
            Vector3 _positionNow = transform.position;
            float x = PlayerPrefs.GetFloat("Player_X",_positionNow.x);
            float y = PlayerPrefs.GetFloat("Player_Y",_positionNow.x);
            float z = PlayerPrefs.GetFloat("Player_Z",_positionNow.z);
            transform.position = new(x,y,z);
        } else {
            if(PlayerPrefs.GetString($"{name}_isEnable", "true") == "false")
            {
                gameObject.SetActive(false);
            } else {
                Vector3 _positionNow = transform.position;
                float x = PlayerPrefs.GetFloat($"{name}_X",_positionNow.x);
                float y = PlayerPrefs.GetFloat($"{name}_Y",_positionNow.x);
                float z = PlayerPrefs.GetFloat($"{name}_Z",_positionNow.z);            
                transform.position = new(x,y,z);
            }
        }
    }
    private void OnDisable() {
        PlayerPrefs.SetString($"{name}_isEnable", "false");
    }
    private void OnApplicationQuit() {
        if(gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetFloat("Player_X", gameObject.transform.position.x);
            PlayerPrefs.SetFloat("Player_Y", gameObject.transform.position.y);
            PlayerPrefs.SetFloat("Player_Z", gameObject.transform.position.z);
        } else {
            Vector3 _positionNow = transform.position;
            PlayerPrefs.SetFloat($"{name}_X",_positionNow.x);
            PlayerPrefs.SetFloat($"{name}_Y",_positionNow.x);
            PlayerPrefs.SetFloat($"{name}_Z",_positionNow.z);
            PlayerPrefs.SetString($"{name}_isEnable", "true");
        }
    }
}
