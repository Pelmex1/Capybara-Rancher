using CapybaraRancher.CustomStructures;
using CustomEventBus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private TMP_Text[] ImageAmount = new TMP_Text[5];
    [SerializeField] private Image EmptyDocker;
    [SerializeField] private Image Background;
    [SerializeField] private Image ImageCrosses;

    [SerializeField] private Image[] Docker;
    private Image[] Crosses = new Image[5];
    private Data[] _data;

    private void Awake()
    {
        EventBus.OnRepaint = Repaint;

    }
    private void Start()
    {
        EventBus.TransitionData.Invoke(_data);
        for (int i = 0; i < _data.Length; i++)
        {
            Crosses[i] = _data[i].Image;
        }
    }

    private void Repaint()
    {
        for (int i = 0; i < _data.Length; i++)
        {
            if (_data[i].InventoryItem != null)
            {
                Crosses[i].sprite = _data[i].InventoryItem.Image;
                Docker[i].sprite = Background.sprite;
                Crosses[i].gameObject.transform.localScale = new Vector2(2f, 2f);
                ImageAmount[i].text = $"{_data[i].Count}";
            }
            else
            {
                Docker[i].sprite = EmptyDocker.sprite;
                Crosses[i].gameObject.transform.localScale = new Vector2(1f, 1f);
                Crosses[i].sprite = ImageCrosses.sprite;
                ImageAmount[i].text = "";
            }
        }
    }
}