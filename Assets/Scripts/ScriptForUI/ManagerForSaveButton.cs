using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManagerForSaveButton : MonoBehaviour, IButtonSave
{
    public string NameOfSave { get; set; }
    public Sprite IconOfSave { get; set; }
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text text;
    public void InitSavePaenl()
    {
        image.sprite = IconOfSave;
        text.text = NameOfSave;
    }
    public void LoadSave()
    {
        EventBus.SetSaveName(NameOfSave);
        PlayerPrefs.GetString("CurrentSave",NameOfSave);
        EventBus.LodingScene.Invoke("Map");
    }
}
