using UnityEngine;
using UnityEngine.UI;

public class InventoryPlayer : MonoBehaviour
{
    [SerializeField] private Transform canonEnter;
    [SerializeField] public GameObject[] Dokers;
    private int index = 0;

    public InventoryItem[] inventory = new InventoryItem[5];
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            index = 0;
            if (!PlayerPrefs.HasKey("NumberDocker"))
            {
                Dokers[index].GetComponent<Image>().color = Color.grey;
                PlayerPrefs.SetInt("NumberDocker", index);
                PlayerPrefs.Save();
            }
            else
            {
                int lastIndex = PlayerPrefs.GetInt("NumberDocker");
                Dokers[lastIndex].GetComponent<Image>().color = Color.white;
                Dokers[index].GetComponent<Image>().color = Color.grey;
                PlayerPrefs.SetInt("NumberDocker", index);
                PlayerPrefs.Save();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            index = 1;
            index = 0;
            if (!PlayerPrefs.HasKey("NumberDocker"))
            {
                Dokers[index].GetComponent<Image>().color = Color.grey;
                PlayerPrefs.SetInt("NumberDocker", index);
                PlayerPrefs.Save();
            }
            else
            {
                int lastIndex = PlayerPrefs.GetInt("NumberDocker");
                Dokers[lastIndex].GetComponent<Image>().color = Color.white;
                Dokers[index].GetComponent<Image>().color = Color.grey;
                PlayerPrefs.SetInt("NumberDocker", index);
                PlayerPrefs.Save();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            index = 2;
            index = 0;
            if (!PlayerPrefs.HasKey("NumberDocker"))
            {
                Dokers[index].GetComponent<Image>().color = Color.grey;
                PlayerPrefs.SetInt("NumberDocker", index);
                PlayerPrefs.Save();
            }
            else
            {
                int lastIndex = PlayerPrefs.GetInt("NumberDocker");
                Dokers[lastIndex].GetComponent<Image>().color = Color.white;
                Dokers[index].GetComponent<Image>().color = Color.grey;
                PlayerPrefs.SetInt("NumberDocker", index);
                PlayerPrefs.Save();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            index = 3;
            index = 0;
            if (!PlayerPrefs.HasKey("NumberDocker"))
            {
                Dokers[index].GetComponent<Image>().color = Color.grey;
                PlayerPrefs.SetInt("NumberDocker", index);
                PlayerPrefs.Save();
            }
            else
            {
                int lastIndex = PlayerPrefs.GetInt("NumberDocker");
                Dokers[lastIndex].GetComponent<Image>().color = Color.white;
                Dokers[index].GetComponent<Image>().color = Color.grey;
                PlayerPrefs.SetInt("NumberDocker", index);
                PlayerPrefs.Save();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            RemoveItem(canonEnter.position + canonEnter.forward);
        }
    }
}
