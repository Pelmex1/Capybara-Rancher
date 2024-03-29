using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Iinstance : MonoBehaviour
{
    public static Iinstance instance;

    public float money;
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
    }
    private void Start() {
        money = PlayerPrefs.GetFloat("Money",0);
    }
    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
//
    //}
    public void SaveData(){
        PlayerPrefs.SetFloat("Money", money);
    } 
    private void OnApplicationQuit() {
        SaveData();
    }
}
