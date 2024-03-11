using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Experimental;

public class MainPanelBuilding : MonoBehaviour
{
    public int IndexPlace;
    public Transform PositionPlace;
    public GameObject FirstPlace;
    public GameObject NewPlace;
    public GameObject ParentObject;
    public UIBilding UIBuilding;

    [SerializeField] private GameObject FarmInfoPanel;
    [SerializeField] private GameObject EnclosureInfoPanel;
    [SerializeField] private GameObject Area;
    [SerializeField] private TMP_Text TextMoney;


/*     private void Start()
    {
        PlayerPrefs.DeleteAll();
    }
 */
    private void LateUpdate()
    {
        TextMoney.text = $"{Iinstance.instance.money}";
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
            Iinstance.instance.money -= 150;
            Destroy(FirstPlace);
            UIBuilding.NewObject = Instantiate(objectWichBuy, PositionPlace, ParentObject);
            if (UIBuilding.NewObject.TryGetComponent<Receptacle>(out var receptacle))
                receptacle.UIBuilding = UIBuilding;
            PlayerPrefs.SetString($"{IndexPlace}", objectWichBuy.name);
            PlayerPrefs.Save();
            UIBuilding.OffBuilding();
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
            UIBuilding.OffBuilding();
        }
        else
        {
            return;
        }
    }
}


