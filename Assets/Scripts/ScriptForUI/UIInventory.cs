using CapybaraRancher.CustomStructures;
using CustomEventBus;
using DevionGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private TMP_Text[] ImageAmount = new TMP_Text[5];
    [SerializeField] private Image EmptyDocker;
    [SerializeField] private Image Background;
    [SerializeField] private Image ImageCrosses;

    [SerializeField] private Image[] _docker;
    private Image[] _crosses = new Image[5];
    private Data[] _data = new Data[5];

    private void Awake()
    {
        EventBus.OnRepaint = Repaint;
        EventBus.WasChangeIndexCell = ChangeDocker;
        for (int i = 0; i < _docker.Length; i++)
            _crosses[i] = _docker[i].gameObject.GetComponentInChildren<Image>();
    }
    private void Start()
    {
        EventBus.TransitionData.Invoke(_data);

        // for (int i = 0; i < _data.Length; i++)
        // {
        //     if (_data[i].InventoryItem == null)
        //         Debug.Log($"Empty{i}");
        //     // _crosses[i] = _data[i].Image;
        // }
    }

    private void ChangeDocker(int lastindex,int index)
    {
        _crosses[lastindex].color = Color.white;
        _crosses[index].color = Color.grey;
    }

    private void Repaint()
    {
        EventBus.TransitionData.Invoke(_data);
        for (int i = 0; i < _data.Length; i++)
        {
            if (_data[i].InventoryItem != null)
            {
                Debug.Log("Work1");
                _crosses[i].sprite = _data[i].InventoryItem.Image;
                _docker[i].sprite = Background.sprite;
                _crosses[i].gameObject.transform.localScale = new Vector2(2f, 2f);
                ImageAmount[i].text = $"{_data[i].Count}";
                return;
            }
            else
            {
                Debug.Log("Work2");
                _docker[i].sprite = EmptyDocker.sprite;
                _crosses[i].gameObject.transform.localScale = new Vector2(1f, 1f);
                _crosses[i].sprite = ImageCrosses.sprite;
                ImageAmount[i].text = "";
                return;
            }
        }
    }
}