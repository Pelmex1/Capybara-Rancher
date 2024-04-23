using CapybaraRancher.CustomStructures;
using System.Collections;
using CustomEventBus;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryPlayer : MonoBehaviour, IInventory
{
    [SerializeField] private BoxCollider canonEnter;

    [SerializeField] private GameObject _panel;

    private int _index = 0;
    private int _localIndex;
    private const float SPEED = 10f;
    private Data _nullChestCell;
    private int n = 0;
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
<<<<<<< HEAD
            Inventory[i].Image = cells[i].Image.sprite;
=======
            Inventory[i].Image = cells[i].Image;
            Inventory[i].SaveCellData = cells[i].SaveCellData;
        }
    }

    private void TransitionData(Data[] data)
    {
        Debug.Log("Work");
        for(int i = 0; i < data.Length; i++)
        {
            data[i].InventoryItem = Inventory[i].InventoryItem;
            data[i].Image = Inventory[i].Image;
            data[i].Count = Inventory[i].Count;
            data[i].SaveCellData = Inventory[i].SaveCellData;
<<<<<<< HEAD
>>>>>>> parent of 1f7d724 (update)
=======
>>>>>>> parent of 1f7d724 (update)
        }
    } 

    public bool AddItemInInventory(InventoryItem inventoryItem)
    {
        EventBus.OnRepaint.Invoke();
        if (Inventory[_index].InventoryItem == null ||
            (Inventory[_index].InventoryItem == inventoryItem && Inventory[_index].Count < 20))
        {
            Inventory[_index].InventoryItem = inventoryItem;
            Inventory[_index]++;
<<<<<<< HEAD
<<<<<<< HEAD
            EventBus.OnRepaint.Invoke(Inventory);
=======
>>>>>>> parent of 1f7d724 (update)
=======
>>>>>>> parent of 1f7d724 (update)
            return true;
        }
        else
        {
            for (int i = 0; i < Inventory.Length; i++)
            {
<<<<<<< HEAD
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
=======
                Inventory[i]++;
                EventBus.PlayerGunAdd();
                return true;
            }
            else if (Inventory[i].InventoryItem == null && _nullChestCell.Equals(null))
            {
                _nullChestCell = Inventory[i];
                _localIndex = i;
                EventBus.PlayerGunAdd();
            }
        }
        if (!_nullChestCell.Equals(null))
        {
            _nullChestCell.InventoryItem = inventoryItem;
            _nullChestCell++;
            Inventory[_localIndex] = _nullChestCell;
            _nullChestCell = new Data();
            return true;
        }
        
<<<<<<< HEAD
>>>>>>> parent of 1f7d724 (update)
=======
>>>>>>> parent of 1f7d724 (update)
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
        float ScrollDelta = Input.mouseScrollDelta.y;
        if (ScrollDelta < 0 && _index < 5 && Time.timeScale == 1f)
            ChangeIndex(-1);
        else if (ScrollDelta > 0 && _index >= 0 && Time.timeScale == 1f)
            ChangeIndex(1);
        _index = IsButton();
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