using System.Collections.Generic;
using CustomEventBus;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject PanelOptions;
    [SerializeField] private GameObject PanelButton;
    [SerializeField] private GameObject PanelMultiplayer;
    [SerializeField] private GameObject PanelLoad;
    [SerializeField] private GameObject PanelNewGame;

    private void Start()
    {
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
