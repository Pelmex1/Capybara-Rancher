using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class LocalOptions : MonoBehaviour
{
    [SerializeField] private Button AudioButton;
    [SerializeField] private Sprite ButtonOnSprite;
    [SerializeField] private Sprite ButtonOffSprite;
    [SerializeField] private AudioSource[] Audio;
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private TMP_Dropdown DropdownScreen;
    [SerializeField] private TMP_Text textQuality;



    private bool isActiveButtonSound;

    private int Quality;
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
            slider.gameObject.SetActive(false);
            isActiveButtonSound = false;
            for (int i = 0; i < Audio.Length; i++)
            {
                Audio[i].enabled = false;
            }
        }
        else
        {
            AudioButton.image.sprite = ButtonOnSprite;
            float SaveValueSlider = PlayerPrefs.GetFloat("SliderVolume");
            slider.gameObject.SetActive(true);
            isActiveButtonSound = true;
            for (int i = 0; i < Audio.Length; i++)
            {
                Audio[i].enabled = true;
                Audio[i].volume = SaveValueSlider;
            }
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
            slider.gameObject.SetActive(true);
            for (int i = 0; i < Audio.Length; i++)
            {
                Audio[i].enabled = true;
            }
            PlayerPrefs.SetInt("isSoundOn", 1);
            isActiveButtonSound = false;
        }
        else
        {
            AudioButton.image.sprite = ButtonOffSprite;
            slider.gameObject.SetActive(false);
            for (int i = 0; i < Audio.Length; i++)
            {
                Audio[i].enabled = false;
            }
            PlayerPrefs.SetInt("isSoundOn", 0);
            isActiveButtonSound = true;
        }
        PlayerPrefs.Save();
    }


    public void CheckSlider()
    {
        for (int i = 0; i < Audio.Length; i++)
        {
            Audio[i].volume = slider.value;
            PlayerPrefs.SetFloat("SliderVolume", Audio[i].volume);
            PlayerPrefs.Save();
        }
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

