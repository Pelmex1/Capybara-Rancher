using UnityEngine;
using UnityEngine.UI;

public class InventoryPlayer : MonoBehaviour
{
    [SerializeField] private Transform canonEnter;
    private int index = 0;

    public InventoryItem[] inventory = new InventoryItem[5];
    public Image[] Dokers;
    public int[] inventoryCount = new int[5];
    public bool WasChange = false;

    public bool AddItemInInventory(InventoryItem inventoryItem)
    {
        WasChange = true;
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == inventoryItem)
            {
                inventoryCount[i]++;
                return true;
            }
        }
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = inventoryItem;
                inventoryCount[i]++;
                return true;
            }
            else continue;
        }
        return false;
    }
    public void RemoveItem(Vector3 pos)
    {
        WasChange = true;
        if (inventory[index] == null)
        {
            return;
        }
        else if (inventoryCount[index] > 1)
        {
            inventoryCount[index]--;
            Instantiate(inventory[index].prefab, pos, Quaternion.identity).GetComponent<Rigidbody>().AddForce(pos, ForceMode.Impulse);
        }
        else
        {
            inventoryCount[index]--;
            Instantiate(inventory[index].prefab, pos, Quaternion.identity).GetComponent<Rigidbody>().AddForce(pos, ForceMode.Impulse);
            inventory[index] = null;
        }
    }
    private void Update()
    {
            KeyCode code = KeyCode.None;
            index = code switch
            {
                KeyCode.Alpha1 => 0,
                KeyCode.Alpha2 => 1,
                KeyCode.Alpha3 => 2,
                KeyCode.Alpha4 => 3,
                _ => 0,
            };

            //Dokers[index - 1].color = Color.white;
            //Dokers[index].color = Color.grey;

            //PlayerPrefs.SetInt("NumberDocker", index);
            //PlayerPrefs.Save();

        if (Input.GetMouseButtonDown(1))
        {
            RemoveItem(canonEnter.position + canonEnter.forward);
        }
    }
}
