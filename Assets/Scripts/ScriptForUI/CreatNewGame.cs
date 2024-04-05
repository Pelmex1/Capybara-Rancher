using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreatNewGame : MonoBehaviour
{

    [SerializeField] private Inventory inventory;

    [SerializeField] private Sprite SelectMod;
    [SerializeField] private Sprite NotSelectMod;
    [SerializeField] private Image[] AllSelectImage = new Image[3];
    [SerializeField] private Image[] SaveIncons;
    [SerializeField] private TMP_Text TextOfMode;
    [SerializeField] private GameObject ObjectIcons;
    [SerializeField] private GameObject PanelNewGame;
    [SerializeField] private GameObject PanelButton;
    [SerializeField] private LorScene lorScene;
    [SerializeField] private TMP_Text TextNameGame;

    public Image SelectIcon;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    private void Start()
    {
        AllSelectImage[0].sprite = SelectMod;
        TextOfMode.text = "Live the life of a Capybara Ranher and explore the wonders of the Robot, Robot Ranger at your own pace.";
        //localItems = inventory.items;
    }

    public void BackToMain()
    {
        PanelNewGame.SetActive(false);
        PanelButton.SetActive(true);
    }

    public void Confirm()
    {
        audioSource.Stop();
        lorScene.OnLorScene();
        PlayerPrefs.DeleteAll();
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
