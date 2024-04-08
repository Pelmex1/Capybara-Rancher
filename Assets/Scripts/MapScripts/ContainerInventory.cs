using CustomEventBus;
using UnityEngine;

public class ContainerInventory : MonoBehaviour
{
    public delegate void HelpUIEnable(bool isEnable);
    public static event HelpUIEnable EnableBaseUI;
    public delegate void AddItemCollection(ChestCell[] chestCells);
    public static event AddItemCollection UpdateInventoryUI;
    public static event AddItemCollection UpdateChestUI;
    private bool _isNearChest = false;
    private readonly ChestCell[] _chestCell = new ChestCell[12];

    private InventoryPlayer playerInventory;
    private void Start() {
        for(int i = 0; i < 12; i++){
            _chestCell[i] = new();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInventory = other.gameObject.GetComponent<InventoryPlayer>();
            EventBus.EnableHelpUi(true);
            _isNearChest = true;
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isNearChest)
        {
            UpdateChestUI(_chestCell);
            UpdateInventoryUI(playerInventory.inventory);
            Cursor.lockState = CursorLockMode.Confined;
            EnableBaseUI.Invoke(true);
            EventBus.EnableHelpUi(false);
        }
    }

    public void Exit()
    {
        Cursor.lockState = CursorLockMode.Locked;
        EnableBaseUI.Invoke(false);
    }
}
