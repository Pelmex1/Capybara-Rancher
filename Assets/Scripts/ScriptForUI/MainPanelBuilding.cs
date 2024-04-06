using UnityEngine;
using TMPro;
using CustomEventBus;

public class MainPanelBuilding : MonoBehaviour
{
    [SerializeField] private GameObject FarmInfoPanel;
    [SerializeField] private GameObject EnclosureInfoPanel;
    [SerializeField] private GameObject Area;
    [SerializeField] private TMP_Text TextMoney;

    public int IndexPlace;
    public Transform PositionPlace;
    public GameObject FirstPlace;
    public GameObject NewPlace;
    public GameObject ParentObject;

    private void LateUpdate()
    {
        TextMoney.text = $"{Iinstance.instance.money}";
    }

    private void OnEnable()
    {
        EventBus.TransitionBuildingData += GetBuildingData;
    }

    private void OnDisable()
    {
        EventBus.TransitionBuildingData -= GetBuildingData;
    }

    private void GetBuildingData(int index, Transform ParentPosition, GameObject FirstObject, GameObject NewObject)
    {
        IndexPlace = index;
        PositionPlace = ParentPosition;
        FirstPlace = FirstObject;
        NewPlace = NewObject != null ? NewObject : NewPlace;
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
                FarmInfoPanel.SetActive(false);
            else
                EnclosureInfoPanel.SetActive(false);
            PanelsInfPlace.SetActive(true);
            PlayerPrefs.SetString("PanelsInfo", PanelsInfPlace.name);
        }
        PlayerPrefs.Save();
    }

    public void Buy(GameObject objectWichBuy)
    {
        if (NewPlace == null && Iinstance.instance.money >= 150)
        {
            Iinstance.instance.money -= 150;
            Destroy(FirstPlace);
            NewPlace = Instantiate(objectWichBuy, PositionPlace, ParentObject);
            if (NewPlace.TryGetComponent<Receptacle>(out var receptacle))
                receptacle.GetData(PositionPlace, NewPlace);
            PlayerPrefs.SetString($"{IndexPlace}", objectWichBuy.name);
            PlayerPrefs.Save();
            EventBus.OffBuilding.Invoke();
        }
        else
        {
            return;
        }
    }

    public void Delte()
    {
        if (NewPlace != null)
        {
            Destroy(NewPlace);
            Instantiate(Area, PositionPlace, ParentObject);
            PlayerPrefs.DeleteKey($"{IndexPlace}");
            PlayerPrefs.Save();
            EventBus.OffBuilding.Invoke();
        }
        else
        {
            return;
        }
    }
}