using System.Collections;
using System.Collections.Generic;
using CapybaraRancher.Interfaces;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class UIForSave : MonoBehaviour
{
    [SerializeField] private Sprite[] _saveImages = new Sprite[5];
    [SerializeField] private GameObject _panelForSave;
    [SerializeField] private Transform _spawnObject;

    private Dictionary<string, Sprite> NameofIcons = new Dictionary<string, Sprite>();
    private int heigh = -125;
    IButtonSave buttonSave;
    private List<string> Saves = new();
    private void Awake()
    {
        Saves = Iinstance.instance.Saves;
        for (int i = 0; i < _saveImages.Length; i++)
            NameofIcons.Add(_saveImages[i].name, _saveImages[i]);
    }

    private void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("SaveCount", 0));
        if (PlayerPrefs.GetInt("SaveCount", 0) != 0)
        {
            for (int i = 0; i < Saves.Count; i++)
            {
                GameObject savePanel = Instantiate(_panelForSave, _spawnObject);
                savePanel.TryGetComponent(out buttonSave);
                if (i >= 0)
                    savePanel.transform.position = new Vector3(savePanel.transform.position.x, savePanel.transform.position.y + heigh, savePanel.transform.position.z);
                buttonSave.IconOfSave = NameofIcons[PlayerPrefs.GetString($"{Saves[i]}SelectIcon")];
                buttonSave.NameOfSave = Saves[i];
                buttonSave.InitSavePaenl();
            }
        }
    }
}
