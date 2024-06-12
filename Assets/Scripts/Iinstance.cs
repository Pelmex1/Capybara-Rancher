using System;
using System.Collections.Generic;
using System.IO;
using CapybaraRancher.Enums;
using CapybaraRancher.EventBus;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Iinstance : MonoBehaviour
{
    public static Iinstance instance;
    public string SaveName;
    public string[] Saves;

    public float Money;
    private readonly Queue<GameObject>[] _movebleobjects = new Queue<GameObject>[(int)TypeGameObject.LastDontToch];
    private List<GameObject> _QueueToDisable = new();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        for (int i = 0; i < _movebleobjects.Length; i++)
        {
            _movebleobjects[i] = new();
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        Money = PlayerPrefs.GetFloat("Money", 0);
        EventBus.AddMoney = (float money) => Money += money;
        EventBus.GetMoney = () => { return Money; };
        SaveName = PlayerPrefs.GetString("CurrentSave","");
        int SaveCount = PlayerPrefs.GetInt("SaveCount");
        for (int i = 0; i < SaveCount; i++)
        {
            Saves[i] = PlayerPrefs.GetString($"Save_{i}");
        }
        EventBus.GetSaveName = () => 
        {
            return SaveName;
        };
        EventBus.SetSaveName = (string name) => 
        {
            SaveName = name;
        };
        if(!Directory.Exists("Save")){
            Directory.CreateDirectory("Save");
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        for(int i = 0; i < _movebleobjects.Length; i++){
            _movebleobjects[i].Clear();
        }
    }
    private void AddInQueue(GameObject localGameObject, TypeGameObject typeGameObject)
    {
        _movebleobjects[(int)typeGameObject].Enqueue(localGameObject);
    }
    private GameObject RemoveFromQueue(TypeGameObject typeGameObject)
    {
        return _movebleobjects[(int)typeGameObject].Dequeue();
    }
    public void SaveData()
    {
        PlayerPrefs.SetFloat("Money", Money);
        for (int i = 0; i < _QueueToDisable.Count; i++)
        {
            PlayerPrefs.SetString($"{_QueueToDisable[i].transform.parent?.name}_{_QueueToDisable[i].name}_isEnable", "false");
        }
        _QueueToDisable.Clear();
    }
    private void OnApplicationQuit()
    {
        SaveData();
    }
    private void OnEnable()
    {
        EventBus.AddInDisable = (GameObject LocalGameObject) => _QueueToDisable.Add(LocalGameObject);
        EventBus.RemoveFromDisable = (GameObject LocalGameObject) => _QueueToDisable.Remove(LocalGameObject);
        EventBus.AddInPool = AddInQueue;
        EventBus.RemoveFromThePool = RemoveFromQueue;
    }
}
