using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Runtime.InteropServices.WindowsRuntime;
using CustomEventBus;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Iinstance : MonoBehaviour
{
    public static Iinstance instance;

    public float Money;
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
    public void SaveData(){
        PlayerPrefs.SetFloat("Money", Money);
    } 
    private void OnApplicationQuit() {
        SaveData();
    }
}
