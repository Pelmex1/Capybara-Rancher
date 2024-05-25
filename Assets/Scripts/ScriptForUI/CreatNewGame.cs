using System;
using CapybaraRancher.EventBus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreatNewGame : MonoBehaviour
{

    [SerializeField] private SavingCellData[] savingInventoryData;
    [SerializeField] private Sprite SelectMod;
    [SerializeField] private Sprite NotSelectMod;
    [SerializeField] private Image[] AllSelectImage = new Image[3];
    [SerializeField] private Image[] SaveIncons;
    [SerializeField] private TMP_Text TextOfMode;
    [SerializeField] private GameObject PanelNewGame;
    [SerializeField] private GameObject PanelButton;
    [SerializeField] private TMP_Text TextNameGame;
    private AudioSource audioSource;

    public Image SelectIcon;


    private void Awake() => audioSource = gameObject.GetComponent<AudioSource>();
    private void Start()
    {
        AllSelectImage[0].sprite = SelectMod;
        TextOfMode.text = "Live the life of a Capybara Ranher and explore the wonders of the Robot, Robot Ranger at your own pace.";
    }

    public void BackToMain()
    {
        PanelNewGame.SetActive(false);
        PanelButton.SetActive(true);
    }

    public void Confirm()
    {
        if (SelectIcon != null && TextOfMode != null && PlayerPrefs.HasKey("KeyMod"))
        {
            audioSource.Stop();
            EventBus.OnLorScene.Invoke();
            for (int i = 0; i < savingInventoryData.Length; i++)
            {
                savingInventoryData[i].InventoryItem = null;
                savingInventoryData[i].Count = 0;
            }
            PlayerPrefs.DeleteAll();
        }
        else return;
    }

    public void ChangeGameMod(string NameMod)
    {
        foreach (var image in AllSelectImage)
        {
            image.sprite = NotSelectMod;
        }

        switch (NameMod)
        {
            case "Adventure":
                AllSelectImage[0].sprite = SelectMod;
                TextOfMode.text = "Live the life of a Capybara Ranher and explore the wonders of the Robot, Robot Ranger at your own pace.";
                break;
            case "Casual":
                AllSelectImage[1].sprite = SelectMod;
                TextOfMode.text = "It is a modified version of Adventure Mode.";
                break;
            case "Rush":
                AllSelectImage[2].sprite = SelectMod;
                TextOfMode.text = "It is a special game mode where the player has three days.";
                break;
            default:
                break;
        }

        PlayerPrefs.SetString("KeyMod", NameMod);
        /*         TextNameMod = NameMod; */
        PlayerPrefs.Save();
    }


    public void SelectIcons(GameObject ObjectButton)
    {
        int indexObject = Convert.ToInt32(ObjectButton.name);
        for (int i = 0; i < SaveIncons.Length; i++)
        {
            SaveIncons[i].color = Color.white;
        }
        SaveIncons[indexObject].color = Color.grey;
        SelectIcon = ObjectButton.GetComponent<Image>();
        //SelectIcon = SelectImage;
    }
}
