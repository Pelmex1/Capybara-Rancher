using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private InventoryPlayer inventoryPlayer;
    [SerializeField] private TMP_Text[] ImageAmount = new TMP_Text[5];
    [SerializeField] private Image EmptyDocker;
    [SerializeField] private Image Background;
    [SerializeField] private Image ImageCrosses;

    [SerializeField] private Image[] Docker;
    private Image[] Crosses = new Image[5];
    private ChestCell[] chestCell;

    private void Awake()
    {
        chestCell = inventoryPlayer.inventory;
        for(int i = 0; i < chestCell.Length;i++)
        {
            Crosses[i] = chestCell[i].image;
        }       
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
        for (int i = 0; i < chestCell.Length; i++)
        {
            if (chestCell[i].inventoryItem != null)
            {
                Crosses[i].sprite = chestCell[i].inventoryItem.image.sprite;
                Docker[i].sprite = Background.sprite;
                Crosses[i].gameObject.transform.localScale = new Vector2(2f,2f); 
                ImageAmount[i].text = $"{chestCell[i].count}";
            }
            else
            {
                Docker[i].sprite = EmptyDocker.sprite;
                Crosses[i].gameObject.transform.localScale = new Vector2(1f,1f); 
                Crosses[i].sprite = ImageCrosses.sprite;
                ImageAmount[i].text = "";
            }
        }
    }
}
