using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LocalMenu : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject PanelOptions;
    [SerializeField] private MovingPlayer movingPlayer;
    [SerializeField] private Image energyBar;
    [SerializeField] private Image hpBar;

    private int indexCheck = 0;
    private float energyMaxValue;
    private float energyCurrentValue;
    private float hpMaxValue;
    private float hpCurrentValue;

    private void Start()
    {
        energyMaxValue = movingPlayer.energyMaxValue;
        hpMaxValue = movingPlayer.hpMaxValue;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (indexCheck == 0)
            {
                Cursor.lockState = CursorLockMode.Confined;
                PausePanel.SetActive(true);
                Time.timeScale = 0;
                indexCheck = 1;
            }
            else if (indexCheck == 1)
            {
                Cursor.lockState = CursorLockMode.Locked;
                PausePanel.SetActive(false);
                Time.timeScale = 1;
                indexCheck = 0;
            }
        }
        UpdateBars();
    }
    private void UpdateBars()
    {
        energyCurrentValue = movingPlayer.energy;
        hpCurrentValue = movingPlayer.hp;
        energyBar.fillAmount = (energyCurrentValue - 5) / (energyMaxValue - 5);
        hpBar.fillAmount = hpCurrentValue / hpMaxValue;
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
