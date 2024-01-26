using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    private void Start()
    {
        LoadFromJson();
    }

    public void SaveToJson()
    {
        string inventoryData = JsonUtility.ToJson(inventory);
        string filePath = Application.persistentDataPath + "/InventoryData.json";
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, inventoryData);
        Debug.Log("Save was secsesfull");
    }

    public void LoadFromJson()
    {
        string filePath = Application.persistentDataPath + "/InventoryData.json";
        string inventoryData = System.IO.File.ReadAllText(filePath);

        inventory = JsonUtility.FromJson<Inventory>(inventoryData);
        Debug.Log("Read save was secsesfull");
    }
}


[System.Serializable]
public class Items
{
    public string NameGame;
    public string GameMod;
    public GameObject icons;

    public Items()
    {

    }
}