using UnityEngine;
using CapybaraRancher.EventBus;

public class ScipTutorial : MonoBehaviour
{
    [SerializeField] private GameObject PanelScipTutorial;
    [SerializeField] private GameObject PanelTutorial;
    [SerializeField] private GameObject Point;
    private void Start()
    {
        int number = PlayerPrefs.GetInt("TutorialComplete", 0);
        Debug.Log($"Tutorial Was ending {number}");
        if (PlayerPrefs.HasKey("TutorialComplete"))
        {
            if (PlayerPrefs.GetInt("TutorialComplete", 0) == 1)
            {
                PanelScipTutorial.SetActive(false);
                PanelTutorial.SetActive(false);
            }
            else Cursor.lockState = CursorLockMode.Confined; Time.timeScale = 0;
        }
        else Cursor.lockState = CursorLockMode.Confined; Time.timeScale = 0;
    }
    public void AnswerNo()
    {
        Cursor.lockState = CursorLockMode.Locked;
        PanelScipTutorial.SetActive(false);
        Point.SetActive(true);
        Time.timeScale = 1;
    }
    public void AnswerYes()
    {
        Cursor.lockState = CursorLockMode.Locked;
        PanelScipTutorial.SetActive(false);
        PanelTutorial.SetActive(false);
        Point.SetActive(true);
        EventBus.ScipTutorial.Invoke();
        Time.timeScale = 1;
    }
}
