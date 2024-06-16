using UnityEngine;
using System;
using System.Collections.Generic;
using CapybaraRancher.Enums;

[CreateAssetMenu(fileName = "KeyBinds", menuName = "KeyBinds")]
public class KeyBinds : ScriptableObject
{
    public Dictionary<ActionType, KeyOrMouseButton> Bindings = new Dictionary<ActionType, KeyOrMouseButton>();

    public void SetBinding(ActionType action, KeyOrMouseButton key) => Bindings[action] = key;

    public KeyOrMouseButton GetBinding(ActionType action) => Bindings[action];

    public string BindToText(ActionType action) => Bindings[action].ToString();
}
