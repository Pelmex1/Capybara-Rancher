using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreatNewGame : MonoBehaviour
{
    readonly SaveData saveData = new();
    [SerializeField] private Inventory inventory;

    [SerializeField] private Sprite SelectMod;
    [SerializeField] private Sprite NotSelectMod;
    [SerializeField] private GameObject[] AllSelectImage;
    [SerializeField] private GameObject[] SaveIncons;
    [SerializeField] private TMP_Text TextOfMode;
    [SerializeField] private GameObject ObjectIcons;
    [SerializeField] private GameObject PanelNewGame;
    [SerializeField] private GameObject PanelButton;
    [SerializeField] private TMP_Text TextNameGame;
    [SerializeField] private int index = 0;

    public GameObject SelectIcon;
    private int FrontIndexOffObject = 0;
    private int BackIndexOffObject = 4;
    private RectTransform rectTransform;
    private string TextNameMod;

    private void Start()
    {
        SaveIncons[0].GetComponent<Image>().color = Color.white;
        SelectIcon = SaveIncons[0];
        rectTransform = ObjectIcons.GetComponent<RectTransform>();
        //localItems = inventory.items;
    }

    public void BackToMain()
    {
        PanelNewGame.SetActive(false);
        PanelButton.SetActive(true);
    }

    public void Confirm()
    {
        try
        {
            Items newItem = new()
            {
                NameGame = TextNameGame.text,
                GameMod = TextNameMod,
                icons = SelectIcon
            };
            inventory.LoadData(newItem);
            saveData.SaveToJson();
            SceneManager.LoadScene("Level");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error in Confirm: {ex.Message}");
        }
    }


    public void ChangeGameMod(string NameMod)
    {
        if (!PlayerPrefs.HasKey("KeyMod"))
        {
            if (NameMod == "Adventure")
            {
                AllSelectImage[0].GetComponent<Image>().sprite = SelectMod;
                TextOfMode.text = "Live the life of a Capybara " +
                    "Ranher and explore the wonders of the Robot, Robot Ranger at your owm pace.";
            }
            else if (NameMod == "Casual")
            {
                AllSelectImage[1].GetComponent<Image>().sprite = SelectMod;
                TextOfMode.text = "It is a modified version of Adventure Mode.";
            }
            else if (NameMod == "Rush")
            {
                AllSelectImage[2].GetComponent<Image>().sprite = SelectMod;
                TextOfMode.text = "It is a special game mode where the player has three days.";
            }
            PlayerPrefs.SetString("KeyMod", NameMod);
        }
        else
        {
            foreach (GameObject name in AllSelectImage)
            {
                if (name.name == PlayerPrefs.GetString("KeyMod"))
                {
                    if (name.name == "Adventure")
                    {
                        AllSelectImage[0].GetComponent<Image>().sprite = NotSelectMod;
                    }
                    else if (name.name == "Casual")
                    {
                        AllSelectImage[1].GetComponent<Image>().sprite = NotSelectMod;
                    }
                    else if (name.name == "Rush")
                    {
                        AllSelectImage[2].GetComponent<Image>().sprite = NotSelectMod;
                    }
                }
            }
            if (NameMod == "Adventure")
            {
                AllSelectImage[0].GetComponent<Image>().sprite = SelectMod;
                TextOfMode.text = "Live the life of a Capybara " +
                    "Ranher and explore the wonders of the Robot, Robot Ranger at your owm pace.";
            }
            else if (NameMod == "Casual")
            {
                AllSelectImage[1].GetComponent<Image>().sprite = SelectMod;
                TextOfMode.text = "It is a modified version of Adventure Mode.";
            }
            else if (NameMod == "Rush")
            {
                AllSelectImage[2].GetComponent<Image>().sprite = SelectMod;
                TextOfMode.text = "It is a special game mode where the player has three days.";
            }
            PlayerPrefs.SetString("KeyMod", NameMod);
        }
        TextNameMod = NameMod;
        PlayerPrefs.Save();
    }

    public void MoveRightIcons()
    {
        if (index <= 3)
        {
            index++;
            SaveIncons[index].GetComponent<Image>().color = Color.white;
            SelectIcon = SaveIncons[index];
            SaveIncons[index - 1].GetComponent<Image>().color = Color.black;
            if (index > 2)
            {
                Vector3 vector3 = new(rectTransform.position.x - 95, rectTransform.position.y, rectTransform.position.z);
                rectTransform.position = vector3;
                SaveIncons[FrontIndexOffObject].GetComponent<Image>().color = Color.black;
                SaveIncons[FrontIndexOffObject].SetActive(false);
                SaveIncons[index].SetActive(true);
                FrontIndexOffObject++;
            }
        }
    }

    public void MoveLeftIcons()
    {
        if (index == 5)
        {
            index--;
        }
        index--;
        if (index >= 0)
        {
            SaveIncons[index].GetComponent<Image>().color = Color.white;
            SelectIcon = SaveIncons[index];
            SaveIncons[index + 1].GetComponent<Image>().color = Color.black;
            if (index > 1)
            {
                Vector3 vector3 = new(rectTransform.position.x + 95, rectTransform.position.y, rectTransform.position.z);
                rectTransform.position = vector3;
                SaveIncons[BackIndexOffObject].GetComponent<Image>().color = Color.black;
                SaveIncons[BackIndexOffObject].SetActive(false);
                SaveIncons[index].SetActive(true);
                SaveIncons[index - 1].SetActive(true);
                SaveIncons[index - 2].SetActive(true);
                BackIndexOffObject--;
            }
        }
    }
}
