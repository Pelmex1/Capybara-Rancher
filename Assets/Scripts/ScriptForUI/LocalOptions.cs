using TMPro;
using UnityEngine;
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

    private bool isActiveButtonSound;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Quality"))
        {
            int Quality = PlayerPrefs.GetInt("Quality");
            QualitySettings.SetQualityLevel(Quality, true);
        }
        else
        {
            QualitySettings.SetQualityLevel(dropdown.value, true);
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
            //for (int i = 0; i < Audio.Length; i++)
            //{
            //    Audio[i].enabled = false;
            //}
        }
        else
        {
            AudioButton.image.sprite = ButtonOnSprite;
            //float SaveValueSlider = PlayerPrefs.GetFloat("SliderVolume");
            slider.gameObject.SetActive(true);
            isActiveButtonSound = true;
            //for (int i = 0; i < Audio.Length; i++)
            //{
            //    Audio[i].enabled = true;
            //    Audio[i].volume = SaveValueSlider;
            //}
        }
    }

    public void CheckDropdown()
    {
        QualitySettings.SetQualityLevel(dropdown.value, true);
        PlayerPrefs.SetInt("Quality", dropdown.value);
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
        if (DropdownScreen.value == 0)
        {
            Screen.fullScreen = true;
            SaveScreen(0, 0);
        }
        if (DropdownScreen.value == 1)
        {
            Screen.SetResolution(1920, 1080, true);
            SaveScreen(1920, 1080);
        }
        if (DropdownScreen.value == 2)
        {
            Screen.SetResolution(1536, 864, true);
            SaveScreen(1536, 864);
        }
        if (DropdownScreen.value == 3)
        {
            Screen.SetResolution(1366, 768, true);
            SaveScreen(1366, 768);
        }
    }

    private void SaveScreen(int ScreenX, int ScreenY)
    {
        PlayerPrefs.SetInt("KeyScreenX", ScreenX);
        PlayerPrefs.SetInt("KeyScreenY", ScreenY);
        PlayerPrefs.Save();
    }
}

