using CustomEventBus;
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

    private void Awake() => EventBus.CheckInventory = Repaint;
    // private void LateUpdate()
    // {
    //     // if (inventoryPlayer.WasChange == true)
    //     // {
    //     //     Repaint();
    //     //     inventoryPlayer.WasChange = false;
    //     // }
    // }

    private void OnEnable()
    {
        EventBus.GetInventoryData += GetInventoryData;
    }

    private void OnDisable()
    {
        EventBus.GetInventoryData += GetInventoryData;
    }

    private void GetInventoryData(ChestCell[] inventory)
    {
        chestCell = inventory;
        for (int i = 0; i < chestCell.Length; i++)
        {
            Crosses[i] = chestCell[i].Image;
        }
    }

    public bool Repaint(bool WasChange)
    {
        if (WasChange)
        {
            for (int i = 0; i < chestCell.Length; i++)
            {
                if (chestCell[i].InventoryItem != null)
                {
                    Crosses[i].sprite = chestCell[i].InventoryItem.Image.sprite;
                    Docker[i].sprite = Background.sprite;
                    Crosses[i].gameObject.transform.localScale = new Vector2(2f, 2f);
                    ImageAmount[i].text = $"{chestCell[i].Count}";
                }
                else
                {
                    Docker[i].sprite = EmptyDocker.sprite;
                    Crosses[i].gameObject.transform.localScale = new Vector2(1f, 1f);
                    Crosses[i].sprite = ImageCrosses.sprite;
                    ImageAmount[i].text = "";
                }
            }
            return true;
        }
        else
            return false;
    }
}
