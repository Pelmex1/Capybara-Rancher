using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstalizeDocker : MonoBehaviour
{
    [SerializeField] public GameObject[] Docker = new GameObject[5];
    [SerializeField] private InventoryPlayer inventoryPlayer;
    [SerializeField] private UIInventory uIInventory;

    private void Awake()
    {
        uIInventory.Docker = Docker;
        inventoryPlayer.Dokers = Docker;
    }
}
