using CapybaraRancher.CustomStructures;
using System.Collections;
using CustomEventBus;
using UnityEngine;

public class InventoryPlayer : MonoBehaviour, IInventory
{
    [SerializeField] private BoxCollider canonEnter;

    [SerializeField] private GameObject _panel;

    private int _index = 0;
    private int _localIndex;
    private const float SPEED = 10f;
    private Data _nullChestCell;
    private int _lastindex;
    public Data[] Inventory { get; set; } = new Data[5];
    private void Awake()
    {
        EventBus.AddItemInInventory = AddItemInInventory;
        //EventBus.TransitionData = TransitionData;
    }

    private void Start()
    {
        ICell[] cells = GetComponentsInChildren<ICell>();
        for (int i = 0; i < cells.Length; i++)
        {
            Inventory[i].InventoryItem = cells[i].InventoryItem;
            Inventory[i].Count = cells[i].Count;
            Inventory[i].Image = cells[i].Image.sprite;
        }
    }

    public bool AddItemInInventory(InventoryItem inventoryItem)
    {
        if (Inventory[_index].InventoryItem == null ||
            (Inventory[_index].InventoryItem == inventoryItem && Inventory[_index].Count < 20))
        {
            Inventory[_index].InventoryItem = inventoryItem;
            Inventory[_index]++;
            EventBus.OnRepaint.Invoke(Inventory);
            return true;
        }
        else
        {
            for (int i = 0; i < Inventory.Length; i++)
            {
                if (Inventory[i].InventoryItem == null || Inventory[i].InventoryItem == inventoryItem && Inventory[i].Count < 20)
                {
                    Inventory[i].InventoryItem = inventoryItem;
                    Inventory[i]++;
                    EventBus.OnRepaint.Invoke(Inventory);
                    return true;
                }
            }
        }
        EventBus.OnRepaint.Invoke(Inventory);
        return false;
    }

    public void RemoveItem(Vector3 spawnPos, Vector3 pos)
    {

        if (Inventory[_index].InventoryItem == null)
        {
            return;
        }
        Inventory[_index]--;
        StartCoroutine(Recherge());
        GameObject localObject = Instantiate(Inventory[_index].InventoryItem.Prefab, spawnPos, Quaternion.identity);
        localObject.GetComponent<Rigidbody>().AddForce(pos, ForceMode.Impulse);
        if (Inventory[_index].Count == 0)
        {
            Inventory[_index].InventoryItem = null;
        }
        EventBus.PlayerGunRemove();
        EventBus.OnRepaint.Invoke(Inventory);
    }
    private void Update()
    {
        _lastindex = _index;
        float ScrollDelta = Input.mouseScrollDelta.y;
        if (ScrollDelta < 0 && _index < 5 && Time.timeScale == 1f)
            ChangeIndex(-1);
        else if (ScrollDelta > 0 && _index >= 0 && Time.timeScale == 1f)
            ChangeIndex(1);
        _index = IsButton();
        EventBus.WasChangeIndexCell.Invoke(_lastindex,_index);
        _lastindex = _index;
        if (Input.GetMouseButtonDown(1))
        {
            RemoveItem(canonEnter.transform.position, -canonEnter.transform.forward * SPEED);
        }
    }
    private int IsButton() => Input.inputString switch
    {
        "1" => 0,
        "2" => 1,
        "3" => 2,
        "4" => 3,
        "5" => 4,
        _ => _index
    };
    private void ChangeIndex(int delta)
    {
        _index += delta;
        _index = Mathf.Clamp(_index, 0, 4);
    }
    private IEnumerator Recherge()
    {
        EventBus.InumeratorIsEnabled(true);
        canonEnter.enabled = false;
        yield return new WaitForSecondsRealtime(3);
        canonEnter.enabled = true;
        EventBus.InumeratorIsEnabled(false);
    }

    //private void OnApplicationQuit()
    //{
    //    for (int i = 0; i < 5; i++)
    //    {
    //        Inventory[i].SaveCellData.InventoryItem = Inventory[i].InventoryItem;
    //        Inventory[i].SaveCellData.Count = Inventory[i].Count;
    //        Inventory[i].SaveCellData.Image.sprite = Inventory[i].Image;
    //    }
    //}
}