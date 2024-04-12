using DevionGames;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using CustomEventBus;

public class LocalOptions : MonoBehaviour
{
    [SerializeField] private GameObject[] OptionsPanels = new GameObject[2];

    [HeaderLine("Main Options")]
    [SerializeField] private Camera Camera;
    [SerializeField] private Button AudioButton;
    [SerializeField] private Sprite ButtonOnSprite;
    [SerializeField] private Sprite ButtonOffSprite;
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private TMP_Dropdown DropdownScreen;

    [SerializeField] private Slider RenderingSlider;
    [SerializeField] private Slider DPISlider;

    [HeaderLine("Music Options")]
    [SerializeField] private AudioMixer audiomixer;

    [SerializeField] private Slider[] audioSliders = new Slider[3];
    [SerializeField] private GameObject OnSoundOptions;


    private bool isActiveButtonSound;

    private int Quality;

    private float[] ArraySave = new float[3];
    private void Awake()
    {
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
        if (!PlayerPrefs.HasKey("Far"))
        {
            float value = 100f;
            RenderingSlider.value = value;
            Camera.farClipPlane = value;
        }
        else
        {
            float value = PlayerPrefs.GetFloat("Far");
            RenderingSlider.value = value;
            Camera.farClipPlane = value;
        }
    }

    private void Start()
    {
        Time.timeScale = 1f;
        GetDataQualty();
        GetDataSound();
        GetDataSensetive();
    }

    private void GetDataQualty()
    {
        if (PlayerPrefs.HasKey("Quality"))
        {
            Quality = PlayerPrefs.GetInt("Quality");
            QualitySettings.SetQualityLevel(Quality, true);
            EventBus.ChnageGrassMod.Invoke();
        }
        else
        {
            EventBus.ChnageGrassMod.Invoke();
            QualitySettings.SetQualityLevel(0, true);
        }
    }

    private void GetDataSound()
    {
        if (PlayerPrefs.GetInt("isSoundOn") == 0)
        {
            AudioButton.image.sprite = ButtonOffSprite;
            isActiveButtonSound = true;
            OnSoundOptions.SetActive(false);
            audiomixer.SetFloat("MasterVolume", -80f);
        }
        else
        {
            AudioButton.image.sprite = ButtonOnSprite;
            isActiveButtonSound = false;
            OnSoundOptions.SetActive(true);
            EventBus.GetMusicValue.Invoke(ArraySave);
            for (int i = 0; i < ArraySave.Length; i++)
                audioSliders[i].value = (ArraySave[i] + 80) / 100;
        }
    }

    private void GetDataSensetive()
    {
        if (!PlayerPrefs.HasKey("DPI"))
        {
            EventBus.WasChangeMouseSensetive.Invoke(50);
            DPISlider.value = 50;
            PlayerPrefs.SetFloat("DPI", 50);
        }
        else
        {
            float value = PlayerPrefs.GetFloat("DPI");
            EventBus.WasChangeMouseSensetive.Invoke(value);
            DPISlider.value = value;
            PlayerPrefs.SetFloat("DPI", value);
        }
        PlayerPrefs.Save();
    }
    public void ChangeMouseSensetive()
    {
        EventBus.WasChangeMouseSensetive.Invoke(DPISlider.value);
        PlayerPrefs.SetFloat("DPI", DPISlider.value);
        PlayerPrefs.Save();
    }

    public void CheckDropdown()
    {
        QualitySettings.SetQualityLevel(dropdown.value, true);
        PlayerPrefs.SetInt("Quality", dropdown.value);
        EventBus.ChnageGrassMod.Invoke();
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
            EventBus.GetMusicValue.Invoke(ArraySave);
            for (int i = 0; i < ArraySave.Length; i++)
                audioSliders[i].value = (ArraySave[i] + 80) / 100;
        }
        else
        {
            AudioButton.image.sprite = ButtonOffSprite;
            OnSoundOptions.SetActive(false);
            PlayerPrefs.SetInt("isSoundOn", 0);
            audiomixer.SetFloat("MasterVolume", -80f);
            isActiveButtonSound = true;
        }
        PlayerPrefs.Save();
    }

    public void ChangePanel(int IndexPanel)
    {
        switch (IndexPanel)
        {
            case 0:
                OptionsPanels[0].SetActive(true);
                OptionsPanels[1].SetActive(false);
                break;
            case 1:
                OptionsPanels[1].SetActive(true);
                OptionsPanels[0].SetActive(false);
                break;
        }
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
        EventBus.SaveMusicValue.Invoke(ArraySave);
    }

    public void ChangeRendering()
    {
        float value = RenderingSlider.value;
        Camera.farClipPlane = value;
        PlayerPrefs.SetFloat("Far", value);
        PlayerPrefs.Save();
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

