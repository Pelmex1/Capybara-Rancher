using CapybaraRancher.CustomStructures;
using CapybaraRancher.EventBus;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    private const float SHIFT_FOR_EXTRASLOT = -80f;

    [SerializeField] private TMP_Text[] ImageAmount;
    [SerializeField] private Sprite ImageCrosses;
    [SerializeField] private GameObject _extraSlotObject;
    [SerializeField] private RectTransform _dockersParentTransform;

    [SerializeField] private Image[] _docker;
    private Image[] _crosses = new Image[5];
    private int _dockersNumber = 5;

    private void Awake()
    {
        EventBus.OnRepaint = Repaint;
        EventBus.WasChangeIndexCell = ChangeDocker;
        EventBus.ExtraSlotUpgrade += AddExtraSlot;
        for (int i = 0; i < _dockersNumber; i++)
            _crosses[i] = _docker[i].gameObject.GetComponentInChildren<Image>();
        AddExtraSlot();
    }
    private void OnDisable()
    {
        EventBus.ExtraSlotUpgrade -= AddExtraSlot;
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
    private void AddExtraSlot()
    {
        if (PlayerPrefs.GetInt("ExtraSlotUpgrade", 0) == 1)
        {
            Image[] newCrosses = new Image[_dockersNumber + 1];
            for (int i = 0; i < _dockersNumber ; i++)
            {
                newCrosses[i] = _crosses[i];
            }
            newCrosses[_dockersNumber] = _docker[_dockersNumber].gameObject.GetComponentInChildren<Image>();
            _crosses = newCrosses;
            _dockersNumber++;
            Vector2 newPos = _dockersParentTransform.anchoredPosition;
            newPos.x += SHIFT_FOR_EXTRASLOT;
            _dockersParentTransform.anchoredPosition = newPos;
            _extraSlotObject.SetActive(true);
        }
    }
}