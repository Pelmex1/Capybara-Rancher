using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Experimental;

public class MainPanelBuilding : MonoBehaviour
{
    public int IndexPlace;
    public Transform PositionPlace;
    private int Money = 1000;
    public GameObject FirstPlace;
    public GameObject NewPlace;
    public GameObject ParentObject;
    public UIBilding UIBuilding;

    [SerializeField] private GameObject FarmInfoPanel;
    [SerializeField] private GameObject EnclosureInfoPanel;
    [SerializeField] private GameObject Area;
    [SerializeField] private TMP_Text TextMoney;


    private void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    private void Update()
    {
        TextMoney.text = $"{Money}";
    }

    public void SelectBuild(GameObject PanelsInfPlace)
    {
        if (!PlayerPrefs.HasKey("PanelsInfo"))
        {
            PanelsInfPlace.SetActive(true);
            PlayerPrefs.SetString("PanelsInfo", PanelsInfPlace.name);
        }
        else
        {
            string Value = PlayerPrefs.GetString("PanelsInfo");
            if (Value == "PaneInfoFarme")
            {
                FarmInfoPanel.SetActive(false);
            }
            else
            {
                EnclosureInfoPanel.SetActive(false);
            }
            PanelsInfPlace.SetActive(true);
            PlayerPrefs.SetString("PanelsInfo", PanelsInfPlace.name);
        }
        PlayerPrefs.Save();
    }

    public void Buy(GameObject objectWichBuy)
    {
        if (UIBuilding.NewObject == null)
        {
            Money -= 150;
            Destroy(FirstPlace);
            UIBuilding.NewObject = Instantiate(objectWichBuy, PositionPlace, ParentObject);
            Receptacle receptacle = UIBuilding.NewObject.GetComponent<Receptacle>();
            if (receptacle != null)
                receptacle.UIBuilding = UIBuilding;
            string NameIndex = $"{IndexPlace}";
            PlayerPrefs.SetString(NameIndex, objectWichBuy.name);
            PlayerPrefs.Save();
        }
        else
        {
            return;
        }
    }

    public void Delte()
    {
        if (UIBuilding.NewObject != null)
        {
            Destroy(UIBuilding.NewObject);
            Instantiate(Area, PositionPlace, ParentObject);
            PlayerPrefs.DeleteKey($"{IndexPlace}");
            PlayerPrefs.Save();
        }
        else
        {
            return;
        }
    }
}


