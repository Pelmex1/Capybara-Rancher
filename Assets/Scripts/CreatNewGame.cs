using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CreatNewGame : MonoBehaviour
{
    [SerializeField] private string InputNameSave;
    [SerializeField] private Sprite SelectMod;
    [SerializeField] private Sprite NotSelectMod;
    [SerializeField] private GameObject[] AllSelectImage;

    private void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    public void ChangeGameMod(string NameMod)
    {
        if (!PlayerPrefs.HasKey("KeyMod"))
        {
            if (NameMod == "Adventure")
            {
                AllSelectImage[0].GetComponent<Image>().sprite = SelectMod;
            }
            else if (NameMod == "Casual")
            {
                AllSelectImage[1].GetComponent<Image>().sprite = SelectMod;
            }
            else if (NameMod == "Rush")
            {
                AllSelectImage[2].GetComponent<Image>().sprite = SelectMod;
            }
            PlayerPrefs.SetString("KeyMod", NameMod);
        }
        else
        {
            foreach(GameObject name in AllSelectImage)
            {
                if(name.name == PlayerPrefs.GetString("KeyMod"))
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
            }
            else if (NameMod == "Casual")
            {
                AllSelectImage[1].GetComponent<Image>().sprite = SelectMod;
            }
            else if (NameMod == "Rush")
            {
                AllSelectImage[2].GetComponent<Image>().sprite = SelectMod;
            }
            PlayerPrefs.SetString("KeyMod", NameMod);
        }
        PlayerPrefs.Save();
    }
}
