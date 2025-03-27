using System;
using CapybaraRancher.Abstraction.CustomStructures;
using CapybaraRancher.Abstraction.Signals.Chest;
using CustomEventBus;
using UnityEngine;
using UnityEngine.UI;

public class ChestsController : MonoBehaviour
{
    [SerializeField] private GameObject UIChestPanel;
    [SerializeField] private GameObject HelpUi;
    [SerializeField] private Image[] InventoryCells;
    [SerializeField] private Image[] ChestCells;
    [SerializeField] private Sprite DefaultSprite;
    private EventBus _eventBus;

    private void Awake() {
        
        EventBus.FoundPos = FoundPos;
    }
    private void SetParent(ISetChestParent iSetChestParent) => iSetChestParent.transform.SetParent(UIChestPanel.transform);
    private void EnableChestUI(IEnableChestUI IEnableChestUIClass) => UIChestPanel.SetActive(IEnableChestUIClass.isEnable);
    private void EnableHelpUi(IEnableHelpUI IEnableHelpUIClass) => HelpUi.SetActive(IEnableHelpUIClass.isEnable);
    private void UpdateCells(IUpdateChestUI updateChest){
        for (int i = 0; i < updateChest.ChestCells.Length; i++)
        {
            if(i < updateChest.InventoryCells.Length)
            {
                InventoryCells[i].sprite = updateChest.InventoryCells[i].InventoryItem?.Image ?? DefaultSprite;
            }
            ChestCells[i].sprite = updateChest.ChestCells[i].InventoryItem?.Image ?? DefaultSprite;
        }
    }
    private (int localIndex2, int localMindex, int newIndex, int newMIndex) FoundPos(Vector3 positionNow, Image image)
    {
        (int,int) localIndex = (-1,-1);
        for (int i = 0; i < ChestCells.Length; i++){
            if(i < InventoryCells.Length){
                if(image == InventoryCells[i]){
                    localIndex = (i,0);
                }
            }
            if(ChestCells[i] == image){
                localIndex = (i,1);
            }
        }
        float pos = 0;
        (int, int) index = (0,0);
        for (int i = 0; i < ChestCells.Length; i++)
        {
            if(i < InventoryCells.Length){
                float tmp1 = Math.Abs(Vector3.Distance(InventoryCells[i].gameObject.transform.position, positionNow));
                if (tmp1 < pos && image != InventoryCells[i])
                {
                    pos = tmp1;
                    index = (i,0);
                } else if(pos == 0 && image != InventoryCells[i]){
                    pos = tmp1;
                    index = (i,0);
                }
            }
            float tmp = Math.Abs(Vector3.Distance(ChestCells[i].gameObject.transform.position, positionNow));
            if (tmp < pos && ChestCells[i] != image)
            {
                pos = tmp;
                index = (i,1);
            }   else if(pos == 0 && ChestCells[i] != image){
                    pos = tmp;
                    index = (i,1);
                }
        }
        return (localIndex.Item1,localIndex.Item2,index.Item1,index.Item2);
    }
    public void Exit()
    {
        Cursor.lockState = CursorLockMode.Locked;
        UIChestPanel.SetActive(false);
    }
    private void OnEnable()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<IUpdateChestUI>(UpdateCells);
        _eventBus.Subscribe<ISetChestParent>(SetParent);
        _eventBus.Subscribe<IEnableChestUI>(EnableChestUI);
        _eventBus.Subscribe<IEnableHelpUI>(EnableHelpUi);
    }
    private void OnDisable()
    {
        _eventBus.UnSubscribe<IUpdateChestUI>(UpdateCells);
        _eventBus.UnSubscribe<ISetChestParent>(SetParent);
        _eventBus.UnSubscribe<IEnableChestUI>(EnableChestUI);
        _eventBus.UnSubscribe<IEnableHelpUI>(EnableHelpUi);
    }
}
