using UnityEngine.UIElements;
using UnityEngine;
using CapybaraRancher.Enums;

public class KeyOrMouseButton
{
    public InputType Type;
    public KeyCode Key;
    public MouseButton MouseButton;

    public KeyOrMouseButton(KeyCode key)
    {
        Type = InputType.Key;
        Key = key;
    }

    public KeyOrMouseButton(MouseButton mouseButton)
    {
        Type = InputType.MouseButton;
        MouseButton = mouseButton;
    }

    public override string ToString()
    {
        return Type == InputType.Key ? Key.ToString() : MouseButton.ToString();
    }
}
