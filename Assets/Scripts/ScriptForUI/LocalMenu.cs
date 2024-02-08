using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalMenu : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject PanelOptions;
    [SerializeField] private MovingPlayer movingPlayer;

    private void Update()
    {
        int indexCheck = 0;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (indexCheck == 0)
            {
                Cursor.lockState = CursorLockMode.Confined;
                PausePanel.SetActive(true);
                Time.timeScale = 0;
                indexCheck += 1;
            }
            else if (indexCheck == 1)
            {
                Cursor.lockState = CursorLockMode.Locked;
                PausePanel.SetActive(false);
                Time.timeScale = 1;
                indexCheck = 0;
            }
        }
    }

    public void OnOptions()
    {
        PanelOptions.SetActive(true);
    }
    public void OffOptions()
    {
        PanelOptions.SetActive(false);
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void SaveQuit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
