using TMPro;
using UnityEngine;

public class TextInit : MonoBehaviour
{
    [SerializeField] private Keycodes _keycodes;
    [SerializeField] private string key;
    private TMP_Text _text;
    private void Start() {
        _text = GetComponent<TMP_Text>();
        _text.text = $"{FoundKey(key)}";
    }
    private KeyCode FoundKey(string key) => 
        key switch 
        {
            "TerminalUse" => _keycodes.TerminalUse,
            "Jump" => _keycodes.Jump,
            "Run" => _keycodes.Run,
            "Pause" => _keycodes.Pause,
            "Eat" => _keycodes.Eat,
            "InfoBook" => _keycodes.Jump,
            "Pull" => _keycodes.Jump,
            "Throw" => _keycodes.Throw,
            _ => KeyCode.None
        };
}
