using UnityEngine;
using UnityEngine.UI;

public class ContainerInventory : MonoBehaviour
{
    public delegate void HelpUIEnable(bool isEnable);
    public static event HelpUIEnable EnableHelpUI;
    public static event HelpUIEnable EnableBaseUI;
    public delegate void AddItemCollection(ChestCell[] chestCells);
    public static event AddItemCollection UpdateInventoryUI;
    public static event AddItemCollection UpdateChestUI;
    private bool isNearChest = false;
    private ChestCell[] chestCell = new ChestCell[12];

    private InventoryPlayer playerInventory;
    private void Start() {
        for(int i = 0; i < 12; i++){
            chestCell[i] = new();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInventory = other.gameObject.GetComponent<InventoryPlayer>();
            EnableHelpUI.Invoke(true);
            isNearChest = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isNearChest = false;
            EnableHelpUI.Invoke(false);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isNearChest)
        {
            UpdateChestUI(chestCell);
            UpdateInventoryUI(playerInventory.inventory);
            Cursor.lockState = CursorLockMode.Confined;
            EnableBaseUI.Invoke(true);
            EnableHelpUI.Invoke(false);
        }
    }

    public void Exit()
    {
        Cursor.lockState = CursorLockMode.Locked;
        EnableBaseUI.Invoke(false);
    }
}
