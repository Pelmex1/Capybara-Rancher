using UnityEngine;
using UnityEngine.UI;

public class InventoryPlayer : MonoBehaviour
{
    [SerializeField] private Transform canonEnter;
    private int index = 0;
    private float speed = 10f;

    public InventoryItem[] inventory = new InventoryItem[5];
    public Image[] Dokers = new Image[5];
    public int[] inventoryCount = new int[5];
    public bool WasChange = false;

    public bool AddItemInInventory(InventoryItem inventoryItem)
    {
        WasChange = true;
        if(inventory[index] == null){
            inventory[index] = inventoryItem;
            inventoryCount[index]++;
            return true;
        } else if(inventory[index] == inventoryItem){
            inventoryCount[index]++;
            return true;
        }
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
        inventoryCount[index]--;
        Instantiate(inventory[index].prefab, canonEnter.position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(pos, ForceMode.Impulse);
        if(inventoryCount[index] == 0)
        {
            inventory[index] = null;
        }
    }
    private void Update()
    {
        int lastindex = index;
        if (Input.GetKey(KeyCode.Alpha1))
            index = 0;
        if (Input.GetKey(KeyCode.Alpha2))
            index = 1;
        if (Input.GetKey(KeyCode.Alpha3))
            index = 2;
        if (Input.GetKey(KeyCode.Alpha4))
            index = 3;
        if (Input.GetKey(KeyCode.Alpha5))
            index = 4;

        //KeyCode code = KeyCode.None;
        //index = code switch
        //{
        //    KeyCode.Alpha1 => 0,
        //    KeyCode.Alpha2 => 1,
        //    KeyCode.Alpha3 => 2,
        //    KeyCode.Alpha4 => 3,
        //    _ => index
        //};
        Dokers[lastindex].color = Color.white;
        Dokers[index].color = Color.grey;
        if (Input.GetMouseButtonDown(1))
        {
            RemoveItem(-canonEnter.forward * speed);
        }
    }
}
