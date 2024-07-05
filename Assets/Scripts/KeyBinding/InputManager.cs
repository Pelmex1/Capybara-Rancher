using System.Text;
using CapybaraRancher.EventBus;
using TMPro;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Keycodes _keycodes;
    [SerializeField] private GameObject BindPanel;
    private KeyCode _localKeycode = KeyCode.None;
    private bool _isFoundKeycode = true;
    private StringBuilder _localkey = null;
    private TMP_Text _localText = null;
    private void Awake() {
        EventBus.ChangeKey = (KeyCode key) => _localKeycode = key;
    }
    private void Update() 
    {
        if(!_isFoundKeycode){
                if(_localKeycode != KeyCode.None){
                BindPanel.SetActive(false);
                _isFoundKeycode = true;
                FoundKey(_localkey.ToString(), _localKeycode);
                _localText.text = $"{_localKeycode}";
                _localKeycode = KeyCode.None;
            }
        } else {
            if(Input.GetKeyDown(_keycodes.TerminalUse))
            {
                EventBus.TerminalUseInput?.Invoke();
            } 
            if(Input.GetKey(_keycodes.Jump))
            {
                EventBus.JumpInput?.Invoke();
            } 
            if(Input.GetKey(_keycodes.Satchel))
            {
                EventBus.SatchelInput?.Invoke();
            } 
            if(Input.GetKey(_keycodes.Run))
            {
                EventBus.RunInput?.Invoke();
            } 
            if(Input.GetKeyDown(_keycodes.Pause))
            {
                EventBus.PauseInput?.Invoke();
            } 
            if(Input.GetKeyDown(_keycodes.Eat))
            {
                EventBus.EatInput?.Invoke();
            } 
            if(Input.GetKeyDown(_keycodes.InfoBook))
            {
                EventBus.InfoBookInput?.Invoke();
            } 
            if(Input.GetKey(_keycodes.Pull))
            {
                EventBus.PullInput?.Invoke();
            } else {
                EventBus.NonPullInput?.Invoke();
            }
            if(Input.GetKeyDown(_keycodes.Throw))
            {
                EventBus.ThrowInput?.Invoke();
            } 
        }
    }
    private void FoundKey(string key, KeyCode keyCode)
    {
        switch (key)
        {
            case "TerminalUse" : 
            _keycodes.TerminalUse = keyCode;
            break;
            case "Jump": 
            _keycodes.Jump = keyCode;
            break;
            case "Run": 
            _keycodes.Run = keyCode;
            break;
            case "Pause": 
            _keycodes.Pause = keyCode;
            break;
            case "Eat":
            _keycodes.Eat = keyCode;
            break;
            case "InfoBook":
            _keycodes.InfoBook = keyCode;
            break;
            case "Pull":
            _keycodes.Pull = keyCode;
            break;
            case "Throw":
            _keycodes.Throw = keyCode;
            break;
            case "Satchel" :
            _keycodes.Satchel = keyCode;
            break;
        };
    }
    public void ChangeKey(string key)
    {
        BindPanel.SetActive(true);
        _isFoundKeycode = false;
        _localkey = new(key);
    }
    public void SentText(TMP_Text text){
        _localText = text;
    }
}
