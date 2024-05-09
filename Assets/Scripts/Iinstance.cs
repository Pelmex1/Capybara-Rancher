using System.Collections.Generic;
using CapybaraRancher.Enums;
using CapybaraRancher.EventBus;
using UnityEngine;

public class Iinstance : MonoBehaviour
{
    public static Iinstance instance;

    public float Money;
    private readonly Queue<GameObject>[] _movebleobjects = new Queue<GameObject>[(int)TypeGameObject.LastDontToch - 1];
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
        //SceneManager.sceneLoaded += OnSceneLoaded;
        EventBus.AddMoney = (float money) => Money += money;
        EventBus.GetMoney = () => { return Money; };
    }
    private void Start()
    {
        Money = PlayerPrefs.GetFloat("Money", 0);
    }
    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
    //
    //}
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
