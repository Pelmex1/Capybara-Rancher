using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private InventoryPlayer inventoryPlayer;
    [SerializeField] public GameObject[] Docker;
    [SerializeField] private TMP_Text[] ImageAmount = new TMP_Text[5];
    [SerializeField] private InventoryItem[] inventory;
    [SerializeField] private int[] inventoryCount;
    [SerializeField] private GameObject EmptyDocker;

    private void Awake()
    {
        inventory = inventoryPlayer.inventory;
        inventoryCount = inventoryPlayer.inventoryCount;
    }

    private void Update()
    {
        if (inventoryPlayer.WasChange == true)
        {
            repaint();
            inventoryPlayer.WasChange = false;
        }
    }

    private void repaint()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null)
            {
                GameObject docker = Docker[i];
                Image newImgae = docker.GetComponent<Image>();
                newImgae = inventory[i].image;
                ImageAmount[i].text = inventoryCount[i].ToString();
            }
            else
            {
                GameObject docker = Docker[i];
                Image newImgae = docker.GetComponent<Image>();
                newImgae = EmptyDocker.GetComponent<Image>();
                ImageAmount[i].text = "";
            }
        }
    }
}
