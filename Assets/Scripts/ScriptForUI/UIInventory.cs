using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private InventoryPlayer inventoryPlayer;
    [SerializeField] private TMP_Text[] ImageAmount = new TMP_Text[5];
    [SerializeField] private InventoryItem[] inventory;
    [SerializeField] private int[] inventoryCount;
    [SerializeField] private Image EmptyDocker;

    [SerializeField] private Image[] Docker;
    public GameObject[] Crosses = new GameObject[5];

    private void Awake()
    {
        for(int i = 0; i <= inventoryPlayer.inventory.Length; i++)
        {
            inventory[i] = inventoryPlayer.inventory[i].inventoryItem;
            inventoryCount[i] = inventoryPlayer.inventory[i].count;
        }
    }

    private void Update()
    {
        if (inventoryPlayer.WasChange == true)
        {
            Repaint();
            inventoryPlayer.WasChange = false;
        }
    }

    private void Repaint()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null)
            {
                Docker[i].sprite = inventory[i].image.sprite;
                Crosses[i].SetActive(false);
                ImageAmount[i].text = $"{inventoryCount[i]}";
            }
            else
            {
                Docker[i].sprite = EmptyDocker.sprite;
                Crosses[i].SetActive(true);
                ImageAmount[i].text = "";
            }
        }
    }
}
