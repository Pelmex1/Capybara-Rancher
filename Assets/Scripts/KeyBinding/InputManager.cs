using System.Text;
using CapybaraRancher.Abstraction.Signals.Input;
using CustomEventBus;
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
    private bool IsActiveChangeKeyCod = false;
    private EventBus _eventBus;
    
    private void ChangeKey(IChangeKey IChangeKeyClass) => _localKeycode = IChangeKeyClass.KeyCode;
    void OnEnable()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<IChangeKey>(ChangeKey);
    }
    void OnDisable()
    {
        _eventBus.UnSubscribe<IChangeKey>(ChangeKey);
    }
    private void Update()
    {
        if (!_isFoundKeycode)
        {
            if (_localKeycode != KeyCode.None)
            {
                BindPanel.SetActive(false);
                _isFoundKeycode = true;
                FoundKey($"{_localkey}", _localKeycode);
                _localText.text = $"{_localKeycode}";
                _localKeycode = KeyCode.None;
                IsActiveChangeKeyCod = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(_keycodes.TerminalUse))
            {
                _eventBus.Invoke<ITerminalUseInput>(new());
            }
            if (Input.GetKey(_keycodes.Jump))
            {
                _eventBus.Invoke<IJumpInput>(new());
            }
            if (Input.GetKeyDown(_keycodes.Satchel))
            {
                _eventBus.Invoke<ISatchelInput>(new());
            }
            if (Input.GetKey(_keycodes.Run))
            {
                _eventBus.Invoke<IRunInput>(new());
            }
            if (Input.GetKeyDown(_keycodes.Pause))
            {
                _eventBus.Invoke<IPauseInput>(new());
            }
            if (Input.GetKeyDown(_keycodes.Eat))
            {
                _eventBus.Invoke<IEatInput>(new());
            }
            if (Input.GetKeyDown(_keycodes.InfoBook))
            {
                _eventBus.Invoke<IInfoBookInput>(new());
            }
            if (Input.GetKey(_keycodes.Pull))
            {
                _eventBus.Invoke<IPullInput>(new());
            }
            else
            {
                _eventBus.Invoke<INonPullInput>(new());
            }
            if (Input.GetKeyDown(_keycodes.Throw))
            {
                _eventBus.Invoke<IThrowInput>(new());
            }
        }
    }
    private void FoundKey(string key, KeyCode keyCode)
    {
        switch (key)
        {
            case "TerminalUse":
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
            case "Satchel":
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
    public void SentText(TMP_Text text)
    {
        _localText = text;
    }
}
