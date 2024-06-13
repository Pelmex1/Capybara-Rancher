using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject _panelWin;
    public void Continue()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        _panelWin.SetActive(false);
    }
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainMenu");
    }
}