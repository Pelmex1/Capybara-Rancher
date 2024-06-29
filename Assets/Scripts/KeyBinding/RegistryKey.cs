using CapybaraRancher.EventBus;
using UnityEngine;

public class RegistryKey : MonoBehaviour
{
    private void OnGUI() {
        Event _event = Event.current;
        if(_event != null){
            if(_event.isKey && _event.keyCode != KeyCode.None){
                EventBus.ChangeKey(_event.keyCode);
            } else if(_event.isMouse){
                EventBus.ChangeKey(IndentifyMouse(_event.button));
            }
        }
    }
    private KeyCode IndentifyMouse(int mouse) =>
        mouse switch {
            0 => KeyCode.Mouse0,
            1 => KeyCode.Mouse1,
            2 => KeyCode.Mouse2,
            3 => KeyCode.Mouse3,
            4 => KeyCode.Mouse4,
            5 => KeyCode.Mouse5,
            6 => KeyCode.Mouse6,
            _ => KeyCode.None
        };
}
