using UnityEngine;
using CapybaraRancher.EventBus;
using UnityEngine.UI;
using TMPro;

public class MainPanelBuilding : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject[] buttonsEnable;
    [SerializeField] private Button[] buttonsDisable;
    [SerializeField] private FarmObject[] farmObjects;
    private (Button button, Button disable, TMP_Text text)[] values;
    private void Awake() {
        EventBus.ActiveFarmPanel = (bool isActive) => _panel.SetActive(isActive);
        EventBus.UpdateFarmButtons = UpdateFarmButtons;
        EventBus.GetBuildings = () => 
        {
            return farmObjects;
        };
    }
    private void Start() {
        values = new (Button button, Button disable, TMP_Text text)[buttonsEnable.Length];
        for(int i = 0; i < buttonsEnable.Length; i++){
            values[i].button = buttonsEnable[i].GetComponent<Button>();
            values[i].button = buttonsDisable[i];
            values[i].text = buttonsEnable[i].GetComponentInChildren<TMP_Text>();
        }
    }
    private void UpdateFarmButtons(bool[] bools){
        for(int i = 0; i < buttonsEnable.Length; i++){
            if(bools[i]){
                values[i].button.enabled = false;
                values[i].text.text = "Bought";
                values[i].disable.enabled = true;
            } else {
                values[i].button.enabled = true;
                values[i].text.text = "Buy";
                values[i].disable.enabled = false;
            }
        }
    }
    public void Buy(int index)
    {
        if(farmObjects[index].Price <= EventBus.GetMoney()){
            values[index].button.enabled = false;
            values[index].text.text = "Bought";
            values[index].disable.enabled = true;
            EventBus.AddMoney(-farmObjects[index].Price);
            EventBus.BuyFarm.Invoke(index, true);
            EventBus.BuildTutorial.Invoke();
        }
    }
    public void Remove(int index){
            values[index].button.enabled = false;
            values[index].text.text = "Bought";
            values[index].disable.enabled = false;
            EventBus.AddMoney(farmObjects[index].Price);
            EventBus.BuyFarm.Invoke(index, false);
    }
}