using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    SaveData savedata = new SaveData();
    Inventory inventory = new Inventory();
    [SerializeField] private GameObject PanelOptions;
    [SerializeField] private GameObject PanelButton;
    [SerializeField] private GameObject PanelMultiplayer;
    [SerializeField] private GameObject PanelLoad;
    [SerializeField] private GameObject PanelNewGame;

    public void PlayNewGame()
    {
        PanelButton.SetActive(false);
        PanelNewGame.SetActive(true);
    } 
    public void PlayContinue()
    {
        savedata.LoadFromJson();
        List<Items> items = inventory.items;
        for (int i = 0; i < items.Count; i++)
        {
            if(i == items.Count -1)
            {
                string name = items[i].NameGame;
                PlayerPrefs.SetString("KeyGame",name);
            }
        }
        SceneManager.LoadScene("Level");
        PlayerPrefs.Save();
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
