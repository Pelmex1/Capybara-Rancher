using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ContainerInventory : MonoBehaviour
{
    [SerializeField] private GameObject HelpUI;
    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private GameObject ChestPanel;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            HelpUI.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            HelpUI.SetActive(false);
        }
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.E)){
            InventoryPanel.SetActive(true);
            ChestPanel.SetActive(true);
            HelpUI.SetActive(false);
        }
    }
}
