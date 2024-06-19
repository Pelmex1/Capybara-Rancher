using System.Text;
using CapybaraRancher.EventBus;
using TMPro;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Keycodes _keycodes;
    private KeyCode _localKeycode = KeyCode.None;
    private bool _isFoundKeycode = true;
    private StringBuilder _localkey = null;
    private TMP_Text _localText = null;
    private void Update() 
    {
        if(!_isFoundKeycode){
            _localKeycode = Event.current.keyCode;
            if(_localKeycode != KeyCode.None){
                _isFoundKeycode = true;
                _localKeycode = KeyCode.None;
                FoundKey(_localkey.ToString(), _localKeycode);
                _localText.text = _localkey.ToString();
            }
        }
        if(Input.GetKeyDown(_keycodes.TerminalUse))
        {
            EventBus.TerminalUseInput.Invoke();
        } 
        if(Input.GetKey(_keycodes.Jump))
        {
            EventBus.JumpInput.Invoke();
        } 
        if(Input.GetKey(_keycodes.Run))
        {
            EventBus.RunInput.Invoke();
        } 
        if(Input.GetKeyDown(_keycodes.Pause))
        {
            EventBus.PauseInput.Invoke();
        } 
        if(Input.GetKeyDown(_keycodes.Eat))
        {
            EventBus.EatInput.Invoke();
        } 
        if(Input.GetKeyDown(_keycodes.InfoBook))
        {
            EventBus.InfoBookInput.Invoke();
        } 
        if(Input.GetKeyDown(_keycodes.Pull))
        {
            EventBus.PullInput.Invoke();
        } 
        if(Input.GetKeyDown(_keycodes.Throw))
        {
            EventBus.ThrowInput.Invoke();
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
            _keycodes.Jump = keyCode;
            break;
            case "Pull":
            _keycodes.Jump = keyCode;
            break;
            case "Throw":
            _keycodes.Throw = keyCode;
            break;
        };
    }
    public void ChangeKey(string key)
    {
        _isFoundKeycode = false;
        _localkey = new(key);
    }
    public void SentText(TMP_Text text){
        _localText = text;
    }
}
