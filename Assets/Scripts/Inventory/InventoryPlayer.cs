using CapybaraRancher.CustomStructures;
using CapybaraRancher.EventBus;
using System.Collections;
using UnityEngine;

public class InventoryPlayer : MonoBehaviour
{
    private const float SPEED = 10f;
    [SerializeField] private BoxCollider canonEnter;
    [SerializeField] private SavingCellData[] _saves;

    private int _index = 0;
    private int _localIndex;
    private Data _nullChestCell;
    private int _lastindex;
    private bool _isEnabledSixCell = false;
    public Data[] Inventory { get; set; }
    private void Awake()
    {
        EventBus.AddItemInInventory = AddItemInInventory;
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("ExtraSlotUpgrade", 0) == 1)
        {
            Inventory = new Data[6];
            _isEnabledSixCell = true;
        }
        else Inventory = new Data[5];
        if (_saves.Length == 6)
        {
            for (int i = 0; i < Inventory.Length; i++)
            {
                Inventory[i].InventoryItem = _saves[i].InventoryItem;
                Inventory[i].Count = _saves[i].Count;
            }
        }
        else throw new System.ArgumentOutOfRangeException();
        EventBus.OnRepaint.Invoke(Inventory);
    }
    public bool AddItemInInventory(InventoryItem inventoryItem)
    {
        EventBus.InventoryTutorial.Invoke();
        if (Inventory[_index].InventoryItem == inventoryItem && Inventory[_index].Count < 20)
        {
            Inventory[_index].InventoryItem ??= inventoryItem;
            Inventory[_index]++;
            EventBus.PlayerGunAdd();
            EventBus.OnRepaint.Invoke(Inventory);
            return true;
        }
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i].InventoryItem == inventoryItem && Inventory[i].Count < 20)
            {
                Inventory[i]++;
                EventBus.PlayerGunAdd();
                EventBus.OnRepaint.Invoke(Inventory);
                return true;
            }
            else if (Inventory[i].InventoryItem == null && _nullChestCell.Equals(null))
            {
                _nullChestCell = Inventory[i];
                _nullChestCell.Image = inventoryItem.Image;
                _localIndex = i;
            }
        }
        if (Inventory[_index].InventoryItem == null)
        {
            Inventory[_index].InventoryItem = inventoryItem;
            Inventory[_index]++;
            _nullChestCell.Image = null;
            EventBus.PlayerGunAdd();
            EventBus.OnRepaint.Invoke(Inventory);
            return true;
        }
        if (!_nullChestCell.Equals(null))
        {
            _nullChestCell.InventoryItem = inventoryItem;
            _nullChestCell++;
            Inventory[_localIndex] = _nullChestCell;
            _nullChestCell = new Data();
            EventBus.OnRepaint.Invoke(Inventory);
            EventBus.PlayerGunAdd();
            return true;
        }
        return false;
    }

    public void RemoveItem(Vector3 spawnPos, Vector3 pos)
    {

        if (Inventory[_index].InventoryItem == null)
        {
            return;
        }
        StartCoroutine(Recherge());
        GameObject localObject = EventBus.RemoveFromThePool(Inventory[_index].InventoryItem.TypeGameObject);
        localObject.transform.position = spawnPos;
        localObject.SetActive(true);
        localObject.GetComponent<Rigidbody>().AddForce(pos, ForceMode.Impulse);
        Inventory[_index]--;
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
        _index = IsButton(Input.inputString, _isEnabledSixCell);
        EventBus.WasChangeIndexCell.Invoke(_lastindex, _index);
        _lastindex = _index;
        if (Input.GetMouseButtonDown(1))
        {
            RemoveItem(canonEnter.transform.position, -canonEnter.transform.forward * SPEED);
        }
    }
    private int IsButton(string Input, bool isSixCell) => (Input, isSixCell) switch
    {
        ("1", _) => 0,
        ("2", _) => 1,
        ("3", _) => 2,
        ("4", _) => 3,
        ("5", _) => 4,
        ("6", true) => 5,
        _ => _index
    };
    private void ChangeIndex(int delta)
    {
        _index += delta;
        _index = Mathf.Clamp(_index, 0, Inventory.Length - 1);
    }
    private IEnumerator Recherge()
    {
        EventBus.InumeratorIsEnabled(true);
        canonEnter.enabled = false;
        yield return new WaitForSecondsRealtime(1);
        canonEnter.enabled = true;
        EventBus.InumeratorIsEnabled(false);
    }
    private void OnEnable()
    {
        EventBus.ExtraSlotUpgrade += AddExtraSlot;
    }
    private void OnDisable()
    {
        EventBus.ExtraSlotUpgrade -= AddExtraSlot;
    }
    private void AddExtraSlot()
    {
        Data[] localInventory = new Data[6];
        for (int i = 0; i < Inventory.Length; i++)
        {
            localInventory[i] = Inventory[i];
        }
        Inventory = localInventory;
        _isEnabledSixCell = true;
    }
    private void OnApplicationQuit()
    {
        for (int i = 0; i < 5; i++)
        {
            _saves[i].InventoryItem = Inventory[i].InventoryItem;
            _saves[i].Count = Inventory[i].Count;
        }
    }
}