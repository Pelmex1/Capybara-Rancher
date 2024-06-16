using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using CapybaraRancher.EventBus;
using CapybaraRancher.Enums;

public class BindingMenu : MonoBehaviour
{
    const string WAITING_TEXT = "Press any key";

    [SerializeField] private TMP_Text[] _keyTexts;

    private TMP_Text _currentBindingText;
    private KeyBinds _keyBinds;
    private string _currentAction;

    private void Start()
    {
        _keyBinds = InputManager.Instance.KeyBindings;
        UpdateBindingTexts();
    }

    private void UpdateBindingTexts()
    {
        foreach (ActionType action in Enum.GetValues(typeof(ActionType)))
        {
            string textToUpdate = _keyBinds.GetBinding(action).ToString();

            int index = (int)action;
            if (index >= 0 && index < _keyTexts.Length)
            {
                _keyTexts[index].text = textToUpdate;
            }
        }
    }

    public void BindNewKey(TMP_Text text)
    {
        _currentBindingText = text;
        _currentAction = text.name;
        text.text = WAITING_TEXT;
    }

    private void OnGUI()
    {
        if (_currentBindingText != null)
        {
            if (Event.current.isKey)
            {
                KeyCode newKey = Event.current.keyCode;
                SetNewBinding(new KeyOrMouseButton(newKey));
            }

            if (Event.current.isMouse)
            {
                MouseButton newMouseButton = (MouseButton)Event.current.button;
                SetNewBinding(new KeyOrMouseButton(newMouseButton));
            }
        }
    }

    private void SetNewBinding(KeyOrMouseButton newBinding)
    {
        if (Enum.TryParse<ActionType>(_currentAction, out ActionType action))
        {
            _keyBinds.SetBinding(action, newBinding);

            _currentBindingText.text = newBinding.ToString();
            _currentBindingText = null;
            _currentAction = null;

            EventBus.KeysRebinded.Invoke();
        }
    }
}
