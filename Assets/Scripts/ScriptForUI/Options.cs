using CapybaraRancher.EventBus;
using DevionGames;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    private const string ScreenX = "KeyScreenX";
    private const string ScreenY = "KeyScreenY";
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
    private float MouseSensitivy;
    private float RenderingDistance;
    private int _screenX = 0;
    private int _screenY = 0;
    private void Awake()
    {
        if (PlayerPrefs.GetInt(ScreenX) == 0)
            Screen.fullScreen = true;
        else
            Screen.SetResolution(PlayerPrefs.GetInt(ScreenX), PlayerPrefs.GetInt(ScreenY), true);
        if (!PlayerPrefs.HasKey("Far"))
        {
            float value = 100f;
            RenderingDistance = value;
            RenderingSlider.value = value;
            Camera.farClipPlane = value;
        }
        else
        {
            float value = PlayerPrefs.GetFloat("Far");
            RenderingDistance = value;
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
    private void OnDisable()
    {
        SaveAllOptionsData();
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
        if (PlayerPrefs.HasKey("isSoundOn"))
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
            DPISlider.value = 100;
            MouseSensitivy = 100;
        }
        else
        {
            DPISlider.value = PlayerPrefs.GetFloat("DPI");
            MouseSensitivy = PlayerPrefs.GetFloat("DPI");
        }
    }
    public void ChangeMouseSensetive()
    {
        MouseSensitivy = DPISlider.value;
    }

    public void CheckDropdown()
    {
        Quality = dropdown.value;
        QualitySettings.SetQualityLevel(Quality, true);
        EventBus.ChnageGrassMod.Invoke();
    }
    public void ButtonSoundOnClick()
    {
        if (PlayerPrefs.HasKey("isSoundOn"))
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
        RenderingDistance = RenderingSlider.value;
        Camera.farClipPlane = RenderingDistance;
    }

    public void ChangeScreen()
    {
        switch (DropdownScreen.value)
        {
            case 0:
                Screen.fullScreen = true;
                break;
            case 1:
                Screen.SetResolution(1920, 1080, true);
                _screenX = 1920;
                _screenY = 1080;
                break;
            case 2:
                Screen.SetResolution(1536, 864, true);
                _screenX = 1536;
                _screenY = 864;
                break;
            case 3:
                Screen.SetResolution(1366, 768, true);
                _screenX = 1366;
                _screenY = 768;
                break;
        }
    }
    private void SaveAllOptionsData()
    {
        PlayerPrefs.SetInt(ScreenX, _screenX);
        PlayerPrefs.SetInt(ScreenY, _screenY);
        PlayerPrefs.SetFloat("Far", RenderingDistance);
        PlayerPrefs.SetInt("Quality", Quality);
        PlayerPrefs.SetFloat("DPI", MouseSensitivy);
        PlayerPrefs.Save();
    }
}
