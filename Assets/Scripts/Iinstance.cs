using System.Collections.Generic;
using CapybaraRancher.Enums;
using CustomEventBus;
using UnityEngine;

public class Iinstance : MonoBehaviour
{
    public static Iinstance instance;

    public float Money;
    private Queue<GameObject>[] _movebleobjects = new Queue<GameObject>[(int)TypeGameObject.LastDontToch - 1];
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
        //SceneManager.sceneLoaded += OnSceneLoaded;
        EventBus.AddMoney = (float money) => Money += money;
        EventBus.GetMoney = () => {return Money;};
    }
    private void Start() {
        Money = PlayerPrefs.GetFloat("Money",0);
    }
    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
//
    //}
    private void AddInQueue(GameObject localGameObject,TypeGameObject typeGameObject){
        _movebleobjects[(int)typeGameObject].Enqueue(localGameObject);
    }
    private GameObject RemoveFromQueue(TypeGameObject typeGameObject) {
        return _movebleobjects[(int)typeGameObject].Dequeue();
    }
    public void SaveData(){
        PlayerPrefs.SetFloat("Money", Money);
    } 
    private void OnApplicationQuit() {
        SaveData();
    }
}
