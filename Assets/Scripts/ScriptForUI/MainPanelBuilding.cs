using UnityEngine;
using CapybaraRancher.EventBus;
using UnityEngine.UI;
using TMPro;

public class MainPanelBuilding : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _HelpPanel;
    [SerializeField] private Button[] buttonsEnable;
    [SerializeField] private Button[] buttonsDisable;
    [SerializeField] private FarmObject[] farmObjects;
    private (Button button, Button disable, TMP_Text text)[] values;
    private void Awake() {
        EventBus.ActiveFarmPanel = (bool isActive) => _panel.SetActive(isActive);
        EventBus.ActiveHelpText = (bool isActive) => _HelpPanel.SetActive(isActive);
        EventBus.UpdateFarmButtons = UpdateFarmButtons;
        EventBus.GetBuildings = () => 
        {
            return farmObjects;
        };
    }
    private void Start() {
        values = new (Button button, Button disable, TMP_Text text)[buttonsEnable.Length];
        for(int i = 0; i < buttonsEnable.Length; i++){
            values[i].button = buttonsEnable[i];
            values[i].disable = buttonsDisable[i];
            values[i].text = buttonsEnable[i].GetComponentInChildren<TMP_Text>();
        }
    }
    private void UpdateFarmButtons(bool[] bools, bool isAnyBuy){
        for(int i = 0; i < buttonsEnable.Length; i++){
            if(bools[i]){
                values[i].button.interactable = false;
                values[i].text.text = "Bought";
                values[i].disable.interactable = true;
            } else if(!isAnyBuy){
                values[i].button.interactable = true;
                values[i].text.text = "Buy";
                values[i].disable.interactable = false;
            } else {
                values[i].button.interactable = false;
                values[i].text.text = "Buy";
                values[i].disable.interactable = false;
            }
        }
    }
    public void Buy(int index)
    {
        if(EventBus.GetMoney() >= farmObjects[index].Price){
            EventBus.AddMoney(-farmObjects[index].Price);
            EventBus.BuyFarm.Invoke(index, true);
            EventBus.BuildTutorial.Invoke();
        }
    }
    public void Remove(int index){
        EventBus.BuyFarm.Invoke(index, false);
    }
    public void Upgrade(int index){
        EventBus.SentUpgrade.Invoke(index);
    }
    public void Exit(){
        Cursor.lockState = CursorLockMode.Locked;
        _panel.SetActive(false);
    }
}