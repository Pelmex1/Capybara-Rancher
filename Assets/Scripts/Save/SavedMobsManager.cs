using System.Collections.Generic;
using UnityEngine;
using CapybaraRancher.Save;
using CapybaraRancher.EventBus;
using UnityEditor.ShaderGraph.Serialization;
using System.Collections;

public class SavedMobsManager : MonoBehaviour
{
    public static SavedMobsManager Instance;

    private const string SAVE_MOBS_KEY = "MobsInPens";
    private const int SAVE_COUNT = 250;

    [SerializeField] private GameObject initCapybara;

    private List<SaveCapybara> _mobsToSave;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        EventBus.GlobalSave += Save;

        _mobsToSave = new List<SaveCapybara>();

        for (int i = 0; i < SAVE_COUNT; i++)
        {
            SaveCapybara save = JSONSerializer.Load<SaveCapybara>($"{EventBus.GetSaveName()}_{SAVE_MOBS_KEY}_{i}");
            if (save != null)
            {
                GameObject capybara = Instantiate(initCapybara);
                capybara.name = "Saved";
                capybara.transform.position = save.Position;
                CapybaraItem item = capybara.GetComponent<CapybaraItem>();
                capybara.GetComponent<CapybaraMovebleObject>().Data = save.Item;
                item.Data1 = save.Data1;
                item.Data2 = save.Data2;
                capybara.SetActive(true);
                FileEditor.DeleteFile($"Save/{EventBus.GetSaveName()}_{SAVE_MOBS_KEY}_{i}.json");
            }
        }
    }
    private void OnDisable()
    {
        EventBus.GlobalSave -= Save;
    }
    private void OnApplicationQuit()
    {
        Save();
    }
    private void Save()
    {
        List<SaveCapybara> listToSave = ClearArray(_mobsToSave);
        for (int i = 0; i < SAVE_COUNT && i < listToSave.Count; i++)
            JSONSerializer.Save($"{EventBus.GetSaveName()}_{SAVE_MOBS_KEY}_{i}", listToSave[i]);
    }
    private List<SaveCapybara> ClearArray(List<SaveCapybara> list)
    {
        HashSet<CapybaraItem> seenElements = new HashSet<CapybaraItem>();
        List<SaveCapybara> uniqueElements = new List<SaveCapybara>();

        foreach (SaveCapybara element in list)
        {
            if (!seenElements.Contains(element.CapybaraItem))
            {
                seenElements.Add(element.CapybaraItem);
                uniqueElements.Add(element);
            }
        }

        return uniqueElements;
    }
    public void AddMobToSave(SaveCapybara mob) => _mobsToSave.Add(mob);
    public void RemoveMobToSave(SaveCapybara mob) => _mobsToSave.Remove(mob);
}
