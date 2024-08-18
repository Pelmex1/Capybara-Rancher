using System.Collections.Generic;
using System.IO;
using CapybaraRancher.Enums;
using CapybaraRancher.EventBus;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Iinstance : MonoBehaviour
{
    public static Iinstance instance;
    public string SaveName;
    public List<string> Saves = new();

    public float Money;
    private readonly Queue<GameObject>[] _movebleobjects = new Queue<GameObject>[(int)TypeGameObject.LastDontToch];
    private List<string> _QueueToDisable = new();
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
        SaveName = PlayerPrefs.GetString("CurrentSave", "");
        int SaveCount = PlayerPrefs.GetInt("SaveCount", 0);
        for (int i = 0; i < SaveCount; i++)
        {
            if(PlayerPrefs.GetString($"Save_{i}") != null)
                Saves.Add(PlayerPrefs.GetString($"Save_{i}"));
        }
        EventBus.GetSaveName = () =>
        {
            return SaveName;
        };
        EventBus.SetSaveName = (string name) =>
        {
            SaveName = name;
            Saves.Add(name);
            for (int i = 0; i < SaveCount; i++)
            {
                PlayerPrefs.SetString($"Save_{i}",Saves[i]);
            }
        };
        if (!Directory.Exists("Save"))
        {
            Directory.CreateDirectory("Save");
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        for (int i = 0; i < _movebleobjects.Length; i++)
        {
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
            PlayerPrefs.SetString($"{_QueueToDisable[i]}_isEnable", "false");
        }
        _QueueToDisable.Clear();
    }
    private void OnApplicationQuit()
    {
        SaveData();
    }
    private void OnEnable()
    {
        EventBus.AddInDisable = (string LocalGameObject) => _QueueToDisable.Add(LocalGameObject);
        EventBus.RemoveFromDisable = (string LocalGameObject) => _QueueToDisable.Remove(LocalGameObject);
        EventBus.AddInPool = AddInQueue;
        EventBus.RemoveFromThePool = RemoveFromQueue;
    }
}
