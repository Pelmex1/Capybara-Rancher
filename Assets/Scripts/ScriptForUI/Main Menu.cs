using CapybaraRancher.EventBus;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject PanelOptions;
    [SerializeField] private GameObject PanelButton;
    [SerializeField] private GameObject PanelMultiplayer;
    [SerializeField] private GameObject PanelLoad;
    [SerializeField] private GameObject PanelNewGame;
    [SerializeField] private Button ContinueButton;

    private void Start()
    {
        if (PlayerPrefs.GetInt("WasCreateNewGame", 0) == 1)
            ContinueButton.interactable = true;
        Time.timeScale = 0;
    }

    public void PlayNewGame()
    {
        PanelButton.SetActive(false);
        PanelNewGame.SetActive(true);
    }
    public void PlayContinue()
    {
        EventBus.LodingScene.Invoke("Map");
    }

    public void OnOptions()
    {
        PanelButton.SetActive(false);
        PanelOptions.SetActive(true);
    }

    public void OffOptions()
    {
        PanelButton.SetActive(true);
        PanelOptions.SetActive(false);
    }

    public void PlayMultiplayer()
    {
        PanelButton.SetActive(false);
        PanelMultiplayer.SetActive(true);
    }
    public void PlayLoad()
    {
        PanelButton.SetActive(false);
        PanelLoad.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
