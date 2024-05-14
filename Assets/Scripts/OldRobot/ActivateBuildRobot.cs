using System.Collections.Generic;
using CapybaraRancher.Enums;
using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivateBuildRobot : MonoBehaviour, ITransitionCrystallData
{
    public bool WasChangeDict { get; set; } = false;
    public Dictionary<string, int> DictionaryCrystall { get; set; } = new();
    [SerializeField] private Image[] _childrenCrystallImage = new Image[3];
    [SerializeField] private TMP_Text[] TextAmountCrystalls = new TMP_Text[3];
    private GameObject ParentObject;
    private int CountCrystall;
    IRobotParts _irobotspart;
    ICrystall _icrystall;

    private void Start()
    {
        ParentObject = gameObject.transform.parent.gameObject;
        EventBus.AddToDict.Invoke();
        SaveAndChangeValueCrysatll();
        UpdateUICrystall();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out _icrystall))
        {
            if (_irobotspart.CheckMoving == false && _icrystall != null)
            {
                string name = _icrystall.NameCrystal;
                if (DictionaryCrystall.ContainsKey(name) && DictionaryCrystall[name] != 0)
                {
                    Destroy(other.gameObject);
                    DictionaryCrystall[name]--;
                    SaveData();
                    UpdateUICrystall();
                    CheckActiveCrystal();
                }
            }
        }
        else return;
    }

    private void UpdateUICrystall()
    {
        TextAmountCrystalls[0].text = DictionaryCrystall["GladeCrystal"].ToString();
        TextAmountCrystalls[1].text = DictionaryCrystall["RockCrystal"].ToString();
        TextAmountCrystalls[2].text = DictionaryCrystall["LeoCrystal"].ToString();
    }

    private void SaveAndChangeValueCrysatll()
    {
        int SaveGladeCr = PlayerPrefs.GetInt($"GladeCrystal{gameObject.name}");
        int SaveRockCr = PlayerPrefs.GetInt($"RockCrystal{gameObject.name}");
        int SaveLeoCr = PlayerPrefs.GetInt($"LeoCrystal{gameObject.name}");
        switch (ParentObject.GetComponent<IMovebleObject>().Data.TypeGameObject)
        {
            case TypeGameObject.FirstPart:
                {
                    if (!PlayerPrefs.HasKey($"GladeCrystal{gameObject.name}"))
                        ChangeDataCrystall(5, 1, 1, true);
                    else
                        ChangeDataCrystall(SaveGladeCr, SaveRockCr, SaveLeoCr, true);
                    break;
                }
            case TypeGameObject.SecondPart:
                {
                    if (!PlayerPrefs.HasKey($"RockCrystal{gameObject.name}"))
                        ChangeDataCrystall(1, 5, 1, true);
                    else
                        ChangeDataCrystall(SaveGladeCr, SaveRockCr, SaveLeoCr, true);
                    break;
                }
            case TypeGameObject.ThirdPart:
                {
                    if (!PlayerPrefs.HasKey($"LeoCrystal{gameObject.name}"))
                        ChangeDataCrystall(1, 1, 5, true);
                    else
                        ChangeDataCrystall(SaveGladeCr, SaveRockCr, SaveLeoCr, true);
                    break;
                }
        }
    }

    private void ChangeDataCrystall(int GladeCrystal, int RockCrystal, int LeoCrystal, bool AnotherChange)
    {
        if (AnotherChange)
        {
            DictionaryCrystall["GladeCrystal"] = GladeCrystal;
            DictionaryCrystall["RockCrystal"] = RockCrystal;
            DictionaryCrystall["LeoCrystal"] = LeoCrystal;
        }
        else
        {
            CountCrystall = name switch
            {
                "GladeCrystal" => GladeCrystal,
                "RockCrystal" => RockCrystal,
                "LeoCrystal" => LeoCrystal,
                _ => CountCrystall
            };
        }

    }

    private void CheckActiveCrystal()
    {
        int AmountCrystal = 0;
        for (int i = 0; i < _childrenCrystallImage.Length; i++)
        {
            string name = _childrenCrystallImage[i].gameObject.name;
            if (DictionaryCrystall.ContainsKey(name))
            {
                switch (ParentObject.GetComponent<IMovebleObject>().Data.TypeGameObject)
                {
                    case TypeGameObject.FirstPart:
                        {
                            ChangeDataCrystall(5, 1, 1, false);
                            AmountCrystal += CountCrystall - DictionaryCrystall[name];
                            break;
                        }
                    case TypeGameObject.SecondPart:
                        {
                            ChangeDataCrystall(1, 5, 1, false);
                            AmountCrystal += CountCrystall - DictionaryCrystall[name];
                            break;
                        }
                    case TypeGameObject.ThirdPart:
                        {
                            ChangeDataCrystall(1, 1, 5, false);
                            AmountCrystal += CountCrystall - DictionaryCrystall[name];
                            break;
                        }
                }
                if (DictionaryCrystall[name] == 0)
                {
                    Color color = _childrenCrystallImage[i].color;
                    color.a = 255f;
                    _childrenCrystallImage[i].color = color;
                }
            }
        }
        if (AmountCrystal == 7)
        {
            _irobotspart.WasBuilding = true;
            EventBus.WasAddingAllCrystall.Invoke();
        }
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt($"GladeCrystal{gameObject.name}", DictionaryCrystall["GladeCrystal"]);
        PlayerPrefs.SetInt($"RockCrystal{gameObject.name}", DictionaryCrystall["RockCrystal"]);
        PlayerPrefs.SetInt($"LeoCrystal{gameObject.name}", DictionaryCrystall["LeoCrystal"]);
        PlayerPrefs.Save();
    }
}
