using System.Collections.Generic;
using System.IO;
using CapybaraRancher.Abstraction.Signals;
using CapybaraRancher.Abstraction.Signals.Pool;
using CapybaraRancher.Enums;
using CustomEventBus;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Iinstance : MonoBehaviour
{
    public static Iinstance instance;
    public string SaveName;
    public List<string> Saves = new();
    public float Money;
    private readonly Queue<GameObject>[] _movebleobjects = new Queue<GameObject>[typeof(TypeGameObject).GetEnumValues().Length];
    private List<string> _QueueToDisable = new();
    private EventBus _eventBus;
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
        SaveName = PlayerPrefs.GetString("CurrentSave", "");
        int SaveCount = PlayerPrefs.GetInt("SaveCount", 0);
        for (int i = 0; i < SaveCount; i++)
        {
            if(PlayerPrefs.GetString($"Save_{i}") != null)
                Saves.Add(PlayerPrefs.GetString($"Save_{i}"));
        }
        if (!Directory.Exists("Save"))
        {
            Directory.CreateDirectory("Save");
        }
    }
    private void SetSaveName(ISetSaveName iSetSaveNameClass)
    {
        SaveName = iSetSaveNameClass.Name;
        Saves.Add(SaveName);
        /*for (int i = 0; i < SaveName; i++)
        {
            PlayerPrefs.SetString($"Save_{i}",Saves[i]);
        }*/
        // Переписать!!!
    }
    private void GetMoney(IGetMoney getMoneyClass) => getMoneyClass.Money = Money;
    private void AddMoney(IAddMoney addMoneyClass) => Money += addMoneyClass.MoneyToAdd;
    private void GetSaveName(IGetSaveName getSaveNameClass) => getSaveNameClass.SaveString = SaveName;
    private void AddToQueue(IAddToQueue iAddToQueueClass)
    {
        _QueueToDisable.Add(iAddToQueueClass.GameObjectName);
        _movebleobjects[(int)iAddToQueueClass.Type].Enqueue(iAddToQueueClass.GameObject);
    }
    private void RemoveFromQueue(IRemoveFromQueue iRemoveFromQueueClass)
    {
        _QueueToDisable.Remove(iRemoveFromQueueClass.GameObjectName);
        iRemoveFromQueueClass.GameObject = _movebleobjects[(int)iRemoveFromQueueClass.Type].Dequeue();
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        for (int i = 0; i < _movebleobjects.Length; i++)
        {
            _movebleobjects[i].Clear();
        }
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
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<IGetMoney>(GetMoney);
        _eventBus.Subscribe<IAddMoney>(AddMoney);
        _eventBus.Subscribe<IGetSaveName>(GetSaveName);
        _eventBus.Subscribe<IAddToQueue>(AddToQueue);
        _eventBus.Subscribe<IRemoveFromQueue>(RemoveFromQueue);
    }
    void OnDisable()
    {
        _eventBus.UnSubscribe<IRemoveFromQueue>(RemoveFromQueue);
        _eventBus.UnSubscribe<IGetMoney>(GetMoney);
        _eventBus.UnSubscribe<IAddMoney>(AddMoney);
        _eventBus.UnSubscribe<IGetSaveName>(GetSaveName);
        _eventBus.UnSubscribe<IAddToQueue>(AddToQueue);
    }
}
