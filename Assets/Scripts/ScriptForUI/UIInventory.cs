using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private InventoryPlayer inventoryPlayer;
    [SerializeField] private TMP_Text[] ImageAmount = new TMP_Text[5];
    [SerializeField] private Image EmptyDocker;

    [SerializeField] private Image[] Docker;
    public GameObject[] Crosses = new GameObject[5];
    private ChestCell[] chestCell;

    private void Awake()
    {
        chestCell = inventoryPlayer.inventory;        
    }

    private void LateUpdate()
    {
        if (inventoryPlayer.WasChange == true)
        {
            Repaint();
            inventoryPlayer.WasChange = false;
        }
    }

    private void Repaint()
    {

        for(int i = 0; i < chestCell.Length; i++)
        {
                if (chestCell[i].inventoryItem != null)
                {                    
                    Docker[i].sprite = chestCell[i].inventoryItem.image.sprite;
                    Crosses[i].SetActive(false);
                    ImageAmount[i].text = $"{chestCell[i].count}";
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
