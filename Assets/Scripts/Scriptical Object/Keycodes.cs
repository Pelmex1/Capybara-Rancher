using UnityEngine;

[CreateAssetMenu(fileName = "Keycodes", menuName = "Capybara-Rancher/Keycodes", order = 0)]
public class Keycodes : ScriptableObject 
{
    public KeyCode Jump;
    public KeyCode Run;
    public KeyCode Pause;
    public KeyCode TerminalUse;
    public KeyCode Eat;
    public KeyCode InfoBook;
    public KeyCode Pull;
    public KeyCode Throw;
}
