using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanelBuilding : MonoBehaviour
{
    public int IndexPlace;
    public Transform PositionPlace;

    [SerializeField] private GameObject FarmInfoPanel;
    [SerializeField] private GameObject EnclosureInfoPanel;


    public void SelectBuild(GameObject PanelsInfPlace)
    {
        if (!PlayerPrefs.HasKey("PanelsInfo"))
        {
            PanelsInfPlace.SetActive(true);
            PlayerPrefs.SetString("PanelsInfo", PanelsInfPlace.name);
        }
        else
        {
            string Value  = PlayerPrefs.GetString("PanelsInfo");
            if(Value == "PaneInfoFarme")
            {
                FarmInfoPanel.SetActive(false);
            }
            else
            {
                EnclosureInfoPanel.SetActive(false);
            }
        }
        PlayerPrefs.Save();
    }

    public void Buy(GameObject objectWichBuy)
    {
        Instantiate(objectWichBuy, PositionPlace);
        string NameIndex = IndexPlace.ToString();
        PlayerPrefs.SetString(NameIndex, objectWichBuy.name);
        PlayerPrefs.Save();
    }
}
