using UnityEngine;
using CapybaraRancher.Enums;
using UnityEngine.UIElements;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public KeyBinds KeyBindings;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);
        DontDestroyOnLoad(this);

        InitializeBindings();
    }

    private void InitializeBindings()
    {
        foreach (ActionType actionType in System.Enum.GetValues(typeof(ActionType)))
            if (!KeyBindings.Bindings.ContainsKey(actionType) || KeyBindings.Bindings[actionType] == null)
                KeyBindings.Bindings[actionType] = GetDefaultBinding(actionType);
    }

    private KeyOrMouseButton GetDefaultBinding(ActionType action)
    {
        switch (action)
        {
            case ActionType.Jump:
                return new KeyOrMouseButton(KeyCode.Space);
            case ActionType.Run:
                return new KeyOrMouseButton(KeyCode.LeftShift);
            case ActionType.Pause:
                return new KeyOrMouseButton(KeyCode.Escape);
            case ActionType.TerminalUse:
                return new KeyOrMouseButton(KeyCode.E);
            case ActionType.Eat:
                return new KeyOrMouseButton(KeyCode.E);
            case ActionType.InfoBook:
                return new KeyOrMouseButton(KeyCode.I);
            case ActionType.Pull:
                return new KeyOrMouseButton(MouseButton.LeftMouse);
            case ActionType.Throw:
                return new KeyOrMouseButton(MouseButton.RightMouse);
            default:
                return null;
        }
    }

    public bool IsActionDown(ActionType action)
    {
        if (!KeyBindings.Bindings.ContainsKey(action))
            return false;

        KeyOrMouseButton binding = KeyBindings.Bindings[action];

        switch (binding.Type)
        {
            case InputType.Key:
                return Input.GetKeyDown(binding.Key);
            case InputType.MouseButton:
                return Input.GetMouseButtonDown((int)binding.MouseButton);
            default:
                return false;
        }
    }

    public bool IsAction(ActionType action)
    {
        if (!KeyBindings.Bindings.ContainsKey(action))
            return false;

        KeyOrMouseButton binding = KeyBindings.Bindings[action];

        switch (binding.Type)
        {
            case InputType.Key:
                return Input.GetKey(binding.Key);
            case InputType.MouseButton:
                return Input.GetMouseButton((int)binding.MouseButton);
            default:
                return false;
        }
    }
}
