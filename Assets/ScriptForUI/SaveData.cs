using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public Inventory inventory = new Inventory();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            LoadFromJson();
        }
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
public class Inventory
{
    public List<Items> items = new List<Items>();
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
    public Items(string nameGame, string gameMod, GameObject icons)
    {
        NameGame = nameGame;
        GameMod = gameMod;
        this.icons = icons;
    }
}


