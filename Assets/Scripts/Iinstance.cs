using CustomEventBus;
using UnityEngine;

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
