using TMPro;
using UnityEngine;
using CapybaraRancher.EventBus;
using CapybaraRancher.Enums;

public class ChangeTextFromBind : MonoBehaviour
{
    const string CHANGEABLE_PART_TEXT = "[KEY]";

    [SerializeField] private ActionType _bind;
    [SerializeField] private TMP_Text _textComponent;

    private KeyBinds _keyBinds;
    private string _part1;
    private string _part2;

    private void Awake() => EventBus.KeysRebinded += ChangeText;

    private void OnDestroy() => EventBus.KeysRebinded -= ChangeText;

    private void Start()
    {
        _keyBinds = InputManager.Instance.KeyBindings;
        int changeablePartPos = _textComponent.text.IndexOf(CHANGEABLE_PART_TEXT);
        _part1 = _textComponent.text.Substring(0, changeablePartPos);
        _part2 = _textComponent.text.Substring(changeablePartPos);
        _part2 = _part2.Replace(CHANGEABLE_PART_TEXT, "");
        ChangeText();
    }

    private void ChangeText()
    {
        string newKey = _keyBinds.BindToText(_bind);
        string newText = new string(_part1 + newKey + _part2);
        _textComponent.text = newText;
    }
}