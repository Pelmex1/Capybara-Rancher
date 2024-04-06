using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using CustomEventBus;

public class LoadingScrip : MonoBehaviour
{

    [Header("Slider")]
    [SerializeField] private GameObject LoadingScreen;
    [SerializeField] private GameObject mainmenu;

    [Header("Slider")]
    [SerializeField] private Slider LoaidingSlader;

    private void OnEnable()
    {
        EventBus.LodingScene += LoadlevelBtn;
    }

    private void OnDisable()
    {
        EventBus.LodingScene -= LoadlevelBtn;
    }

    public void LoadlevelBtn(string levelToLoad)
    {
        mainmenu.SetActive(false);
        LoadingScreen.SetActive(true);

        StartCoroutine(LoadSceneASync(levelToLoad));
    }

    IEnumerator LoadSceneASync(string LevelToLoad)
    {
        AsyncOperation loadOperetion = SceneManager.LoadSceneAsync(LevelToLoad);

        while (!loadOperetion.isDone)
        {
            float progress = Math.Clamp(loadOperetion.progress, 0.1f, 0.9f);
            LoaidingSlader.value = progress;
            yield return null;
        }
    }

}
