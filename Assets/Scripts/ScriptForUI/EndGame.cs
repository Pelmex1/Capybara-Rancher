using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject _panelWin;
    public void Continue()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 1;
        _panelWin.SetActive(false);
    }
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Map");
    }
}