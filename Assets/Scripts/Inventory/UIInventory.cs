using CapybaraRancher.CustomStructures;
using CapybaraRancher.EventBus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private TMP_Text[] ImageAmount = new TMP_Text[5];
    [SerializeField] private Sprite ImageCrosses;

    [SerializeField] private Image[] _docker;
    private readonly Image[] _crosses = new Image[5];

    private void Awake()
    {
        EventBus.OnRepaint = Repaint;
        EventBus.WasChangeIndexCell = ChangeDocker;
        for (int i = 0; i < _docker.Length; i++)
            _crosses[i] = _docker[i].gameObject.GetComponentInChildren<Image>();
    }
    private void ChangeDocker(int lastindex,int index)
    {
        _crosses[lastindex].color = Color.white;
        _crosses[index].color = Color.grey;
    }

    private void Repaint(Data[] _data)
    {
        for (int i = 0; i < _data.Length; i++)
        {
            if (_data[i].InventoryItem != null && _data[i].InventoryItem.Image != _docker[i])
            {
                _docker[i].sprite = _data[i].InventoryItem.Image;
               _crosses[i].gameObject.transform.localScale = new Vector2(2f, 2f);
                ImageAmount[i].text = $"{_data[i].Count}";
            }
            else
            {
                _crosses[i].gameObject.transform.localScale = new Vector2(1f, 1f);
                _crosses[i].sprite = ImageCrosses;
                ImageAmount[i].text = "";
                
            }
        }
       return;
    }
}