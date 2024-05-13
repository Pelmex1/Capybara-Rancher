using CapybaraRancher.CustomStructures;
using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;
using UnityEngine;

public class ContainerInventory : MonoBehaviour
{
    private bool _isNearChest = false;
    private readonly Data[] _chestCell = new Data[11];
    private IInventoryPlayer _inventoryPlayer;
    private void Start() {
        for(int i = 0; i < _chestCell.Length; i++){
            _chestCell[i] = new();
        }
    }
    private void Update()
    {
        if (_isNearChest && Input.GetKeyDown(KeyCode.E))
        {
            Cursor.lockState = CursorLockMode.Confined;
            EventBus.EnableChestUi(true);
            EventBus.EnableHelpUi(false);
            EventBus.UpdateChestUI(_chestCell,_inventoryPlayer.Inventory);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            _inventoryPlayer = other.GetComponent<IInventoryPlayer>();
            _isNearChest = true;
            EventBus.EnableHelpUi(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isNearChest = false;
            EventBus.EnableHelpUi(false);
        }
    }

}
