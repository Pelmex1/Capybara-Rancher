using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject PanelOptions;
    [SerializeField] private GameObject PanelButton;
    [SerializeField] private GameObject PanelMultiplayer;
    [SerializeField] private GameObject PanelLoad;

    public void PlayNewGame()
    {
        SceneManager.LoadScene("Level 1");
    } 
    public void PlayContinue()
    {
        SceneManager.LoadScene("Level 1");
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