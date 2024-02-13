using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Experimental.GraphView;

public class MainPanelBuilding : MonoBehaviour
{
    public int IndexPlace;
    public Transform PositionPlace;
    private int Money = 1000;
    public GameObject FirstPlace;

    [SerializeField] private GameObject FarmInfoPanel;
    [SerializeField] private GameObject EnclosureInfoPanel;
    [SerializeField] private GameObject Area;
    [SerializeField] private TMP_Text TextMoney;

    public Dictionary<int, GameObject> DictPlaces = new();

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
        Destroy(FirstPlace);
        Money -= 150;
        PlayerPrefs.SetInt("IndexPlace", IndexPlace);
        GameObject NewPlace = Instantiate(objectWichBuy, PositionPlace);
        if (NewPlace.name == "Farm(Clone)")
        {
            NewPlace.GetComponent<MovingBetween>().DictPlaces = DictPlaces;
        }
        string NameIndex = $"{IndexPlace}";
        if (DictPlaces[IndexPlace] = null)
        {
            DictPlaces.Add(IndexPlace, NewPlace);
        }
        else
        {
            DictPlaces[IndexPlace] = NewPlace;
        }
        PlayerPrefs.SetString(NameIndex, objectWichBuy.name);
        PlayerPrefs.Save();
    }

    public void Delte()
    {
        GameObject DeleteObject = DictPlaces[IndexPlace];

        if (DeleteObject != null)
        {
            Debug.Log(DeleteObject.name);
            DestroyImmediate(DeleteObject, true);
            DictPlaces[IndexPlace] = null;
            Instantiate(Area, PositionPlace);
            PlayerPrefs.DeleteKey($"{IndexPlace}");
            PlayerPrefs.Save();
        }
    }

}


