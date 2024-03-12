using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerInventory : MonoBehaviour
{
    [SerializeField] private GameObject HelpUI;
    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private GameObject ChestPanel;
    public delegate void AddItemCollection(ChestCell[] chestCells);
    public static event AddItemCollection UpdateInventoryUI;
    public static event AddItemCollection UpdateChestUI;
    private bool isNearChest = false;
    private readonly ChestCell[] chestCell = new ChestCell[20];

    private InventoryPlayer playerInventory;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            playerInventory = other.gameObject.GetComponent<InventoryPlayer>();
            HelpUI.SetActive(true);
            isNearChest = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            isNearChest = false;
            HelpUI.SetActive(false);
        }
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.E) && isNearChest){
            UpdateChestUI(chestCell);
            UpdateInventoryUI(playerInventory.inventory);
            Cursor.lockState = CursorLockMode.Confined;
            InventoryPanel.SetActive(true);
            ChestPanel.SetActive(true);
            HelpUI.SetActive(false);
        }
    }
    
    public void Exit(){
        Cursor.lockState = CursorLockMode.Locked;
        InventoryPanel.SetActive(false);
        ChestPanel.SetActive(false);
    } 
}
