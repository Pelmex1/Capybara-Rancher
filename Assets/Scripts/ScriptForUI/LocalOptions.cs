using DevionGames;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class LocalOptions : MonoBehaviour
{
    [SerializeField] private Button AudioButton;
    [SerializeField] private Sprite ButtonOnSprite;
    [SerializeField] private Sprite ButtonOffSprite;
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private TMP_Dropdown DropdownScreen;
    [SerializeField] private TMP_Text textQuality;

    [HeaderLine("Music Options")]
    [SerializeField] private AudioMixer audiomixer;

    [SerializeField] private Slider[] audioSliders = new Slider[3];
    [SerializeField] private GameObject OnSoundOptions;


    private bool isActiveButtonSound;

    private int Quality;

    private float[] ArraySave = new float[3];
    private void Awake()
    {
        if (PlayerPrefs.HasKey("Quality"))
        {
            Quality = PlayerPrefs.GetInt("Quality");
            QualitySettings.SetQualityLevel(Quality, true);
            EventBus.eventBus.ChnageGrassMod.Invoke();
            /*             switch (Quality)
                        {
                            case 1:
                                Debug.Log(Quality);
                                textQuality.text = "Low";
                                break;
                            case 2:
                                Debug.Log(Quality);
                                textQuality.text = "Medium";
                                break;
                            case 3:
                                Debug.Log(Quality);
                                textQuality.text = "High";
                                break;
                        } */
        }
        else
        {
            textQuality.text = "Low";
            QualitySettings.SetQualityLevel(0, true);
        }
        if (PlayerPrefs.GetInt("KeyScreenX") == 0)
        {
            Screen.fullScreen = true;
        }
        else
        {
            int ScreenX = PlayerPrefs.GetInt("KeyScreenX");
            int ScreenY = PlayerPrefs.GetInt("KeyScreenY");
            Screen.SetResolution(ScreenX, ScreenY, true);
        }
    }

    private void Start()
    {
        Time.timeScale = 1f;
        if (PlayerPrefs.GetInt("isSoundOn") == 0)
        {
            AudioButton.image.sprite = ButtonOffSprite;
            isActiveButtonSound = true;
            OnSoundOptions.SetActive(false);
            audiomixer.SetFloat("MasterVolume", 0f);
        }
        else
        {
            AudioButton.image.sprite = ButtonOnSprite;
            isActiveButtonSound = false;
            OnSoundOptions.SetActive(true);
            EventBus.eventBus.GetMusicValue.Invoke(ArraySave);
            for (int i = 0; i < ArraySave.Length; i++)
                audioSliders[i].value = (ArraySave[i] + 80) / 100;
        }
    }

    public void CheckDropdown()
    {
        QualitySettings.SetQualityLevel(dropdown.value, true);
        PlayerPrefs.SetInt("Quality", dropdown.value);
        switch (Quality)
        {
            case 1:
                textQuality.text = "Low";
                break;
            case 2:
                textQuality.text = "Medium";
                break;
            case 3:
                textQuality.text = "High";
                break;
        }
        EventBus.eventBus.ChnageGrassMod.Invoke();
        PlayerPrefs.Save();
    }
    public void ButtonSoundOnClick()
    {
        if (isActiveButtonSound)
        {
            AudioButton.image.sprite = ButtonOnSprite;
            OnSoundOptions.SetActive(true);
            PlayerPrefs.SetInt("isSoundOn", 1);
            isActiveButtonSound = false;
        }
        else
        {
            AudioButton.image.sprite = ButtonOffSprite;
            OnSoundOptions.SetActive(false);
            PlayerPrefs.SetInt("isSoundOn", 0);
            isActiveButtonSound = true;
        }
        PlayerPrefs.Save();
    }


    public void CheckSlider(string NameMixer)
    {
        if (NameMixer != "Effects")
        {
            if (NameMixer == "MasterVolume")
            {
                ArraySave[0] = (audioSliders[0].value * 100f) - 80f;
                audiomixer.SetFloat(NameMixer, ArraySave[0]);
            }
            else
            {
                ArraySave[1] = (audioSliders[1].value * 100f) - 80f;
                audiomixer.SetFloat(NameMixer, ArraySave[1]);
            }
        }
        else
        {
            ArraySave[2] = (audioSliders[2].value * 100f) - 80f;
            audiomixer.SetFloat("SFXVolume", ArraySave[2]);
            audiomixer.SetFloat("AmbienceVolume", ArraySave[2]);
            audiomixer.SetFloat("PlayerVolume", ArraySave[2]);
            audiomixer.SetFloat("CapybaraVolume", ArraySave[2]);
        }
        EventBus.eventBus.SaveMusicValue.Invoke(ArraySave);
    }

    public void ChangeScreen()
    {
        int screenX = 0;
        int screenY = 0;

        switch (DropdownScreen.value)
        {
            case 0:
                Screen.fullScreen = true;
                break;
            case 1:
                Screen.SetResolution(1920, 1080, true);
                screenX = 1920;
                screenY = 1080;
                break;
            case 2:
                Screen.SetResolution(1536, 864, true);
                screenX = 1536;
                screenY = 864;
                break;
            case 3:
                Screen.SetResolution(1366, 768, true);
                screenX = 1366;
                screenY = 768;
                break;
        }

        SaveScreen(screenX, screenY);
    }



    private void SaveScreen(int ScreenX, int ScreenY)
    {
        PlayerPrefs.SetInt("KeyScreenX", ScreenX);
        PlayerPrefs.SetInt("KeyScreenY", ScreenY);
        PlayerPrefs.Save();
    }
}

