using CapybaraRancher.CustomStructures;
using CustomEventBus;
<<<<<<< HEAD:Assets/Scripts/Inventory/UIInventory.cs
using DevionGames;
using System.Xml.Linq;
=======
>>>>>>> parent of 1f7d724 (update):Assets/Scripts/ScriptForUI/UIInventory.cs
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private TMP_Text[] ImageAmount = new TMP_Text[5];
    [SerializeField] private Sprite ImageCrosses;

    [SerializeField] private Image[] _docker;
    private Image[] _crosses = new Image[5];
<<<<<<< HEAD:Assets/Scripts/Inventory/UIInventory.cs

    private void Awake()
    {
        EventBus.OnRepaint = Repaint;
        EventBus.WasChangeIndexCell = ChangeDocker;
        for (int i = 0; i < _docker.Length; i++)
            _crosses[i] = _docker[i].gameObject.GetComponentInChildren<Image>();
    }
    private void Start()
    {
=======
    private Data[] _data;
    private void Start()
    {
        EventBus.TransitionData.Invoke(_data);
        if (_data == null)
        {
            Debug.Log("WasTransitionData");
            for (int i = 0; i < _data.Length; i++)
            {

                _crosses[i] = _data[i].Image;
            }
        }

>>>>>>> parent of 1f7d724 (update):Assets/Scripts/ScriptForUI/UIInventory.cs
    }

    private void OnEnable()
    {
        EventBus.OnRepaint += Repaint;
    }
    private void OnDisable()
    {
        EventBus.OnRepaint -= Repaint;
    }

    private void Repaint(Data[] _data)
    {
<<<<<<< HEAD:Assets/Scripts/Inventory/UIInventory.cs
=======
        Debug.Log("Work1");
>>>>>>> parent of 1f7d724 (update):Assets/Scripts/ScriptForUI/UIInventory.cs
        for (int i = 0; i < _data.Length; i++)
        {
            if (_data[i].InventoryItem != null && _data[i].InventoryItem.Image != _docker[i])
            {
<<<<<<< HEAD:Assets/Scripts/Inventory/UIInventory.cs
                _docker[i].sprite = _data[i].InventoryItem.Image;
               _crosses[i].gameObject.transform.localScale = new Vector2(2f, 2f);
=======

                _crosses[i].sprite = _data[i].InventoryItem.Image;
                _docker[i].sprite = Background.sprite;
                _crosses[i].gameObject.transform.localScale = new Vector2(2f, 2f);
>>>>>>> parent of 1f7d724 (update):Assets/Scripts/ScriptForUI/UIInventory.cs
                ImageAmount[i].text = $"{_data[i].Count}";
            }
            else
            {
                _crosses[i].gameObject.transform.localScale = new Vector2(1f, 1f);
                _crosses[i].sprite = ImageCrosses;
                ImageAmount[i].text = "";
<<<<<<< HEAD:Assets/Scripts/Inventory/UIInventory.cs
                
=======
>>>>>>> parent of 1f7d724 (update):Assets/Scripts/ScriptForUI/UIInventory.cs
            }
        }
       return;
    }

   
}