using CapybaraRancher.Interfaces;
using UnityEngine;
namespace CapybaraRancher.Save
{
    using CapybaraRancher.EventBus;
    public class SavePosition : MonoBehaviour, IService
    {
        private bool _isOnAplicationQuit = false;
        private bool _oneStart = false;
        private string _saveName;
        private void OnEnable()
        {
            
            try
            {
                EventBus.RemoveFromDisable($"{transform.parent?.name}_{name}");
            } catch {}
            EventBus.GlobalSave += Save;
        }
        public void Init(){
            Start();
        }
        private void Start()
        {
            _saveName = EventBus.GetSaveName();
            if(!_oneStart){
                _oneStart = true;
                if (gameObject.CompareTag("Player"))
                {
                    Vector3 _positionNow = transform.position;
                    float x = PlayerPrefs.GetFloat($"{_saveName}_Player_X", _positionNow.x);
                    float y = PlayerPrefs.GetFloat($"{_saveName}_Player_Y", _positionNow.y);
                    float z = PlayerPrefs.GetFloat($"{_saveName}_Player_Z", _positionNow.z);
                    transform.position = new(x, y, z);
                    x = PlayerPrefs.GetFloat($"{_saveName}_Player_R_X", transform.rotation.x);
                    y = PlayerPrefs.GetFloat($"{_saveName}_Player_R_Y", transform.rotation.y);
                    z = PlayerPrefs.GetFloat($"{_saveName}_Player_R_Z", transform.rotation.z);
                    float w = PlayerPrefs.GetFloat($"{_saveName}_Player_R_W", transform.rotation.w);
                    transform.rotation = new(x, y, z, w);
                }   
                else
                {
                    if (PlayerPrefs.GetString($"{_saveName}_{transform.parent?.name}_{name}_isEnable", "true") == "false")
                    {
                        EventBus.AddInPool(gameObject, GetComponent<IMovebleObject>().Data.TypeGameObject);
                        gameObject.SetActive(false);
                    }
                    else
                    {
                        Vector3 _positionNow = transform.position;
                        float x = PlayerPrefs.GetFloat($"{_saveName}_{transform.parent?.name}_{name}_X", _positionNow.x);
                        float y = PlayerPrefs.GetFloat($"{_saveName}_{transform.parent?.name}_{name}_Y", _positionNow.y);
                        float z = PlayerPrefs.GetFloat($"{_saveName}_{transform.parent?.name}_{name}_Z", _positionNow.z);
                        Vector3 localPosition = new(x, y, z);
                        transform.position = transform.position == localPosition ? transform.position : localPosition;
                    }
                }
            }
        }
        private void OnDisable()
        {
            if (!_isOnAplicationQuit)
                EventBus.AddInDisable($"{transform.parent?.name}_{name}");
            Save();
            EventBus.GlobalSave -= Save;
        }
        private void OnApplicationQuit()
        {
            Save();
            EventBus.GlobalSave -= Save;
        }
        private void Save()
        {
            _isOnAplicationQuit = true;
            if (gameObject.CompareTag("Player"))
            {
                PlayerPrefs.SetFloat($"{_saveName}_Player_X", transform.position.x);
                PlayerPrefs.SetFloat($"{_saveName}_Player_Y", transform.position.y);
                PlayerPrefs.SetFloat($"{_saveName}_Player_Z", transform.position.z);
                PlayerPrefs.SetFloat($"{_saveName}_Player_R_X", transform.rotation.x);
                PlayerPrefs.SetFloat($"{_saveName}_Player_R_Y", transform.rotation.y);
                PlayerPrefs.SetFloat($"{_saveName}_Player_R_Z", transform.rotation.z);
                PlayerPrefs.SetFloat($"{_saveName}_Player_R_W", transform.rotation.w);
            }
            else
            {
                Vector3 _positionNow = transform.position;
                PlayerPrefs.SetFloat($"{_saveName}_{transform.parent?.name}_{name}_X", _positionNow.x);
                PlayerPrefs.SetFloat($"{_saveName}_{transform.parent?.name}_{name}_Y", _positionNow.y);
                PlayerPrefs.SetFloat($"{_saveName}_{transform.parent?.name}_{name}_Z", _positionNow.z);
                PlayerPrefs.SetString($"{_saveName}_{transform.parent?.name}_{name}_isEnable", "true");
            }
            PlayerPrefs.Save();
        }
    }
}