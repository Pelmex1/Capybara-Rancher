using System.Collections;
using System.Collections.Generic;
using CapybaraRancher.Enums;
using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivateBuildRobot : MonoBehaviour, ITransitionCrystallData
{
    private const string Glade = "GladeCrystal";
    private const string Rock = "RockCrystal";
    private const string Leo = "LeoCrystal";
    public bool WasChangeDict { get; set; } = false;
    public Dictionary<string, int> DictionaryCrystall { get; set; } = new();
    [SerializeField] private Image[] _childrenCrystallImage = new Image[3];
    [SerializeField] private TMP_Text[] TextAmountCrystalls = new TMP_Text[3];
    [SerializeField] private GameObject Effect;
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
        if (other.CompareTag("movebleObject") && other.TryGetComponent(out _icrystall) && ParentObject.TryGetComponent<IRobotParts>(out _irobotspart))
        {
            if (_irobotspart.CheckMoving == false && _icrystall != null)
            {
                string name = _icrystall.NameCrystal;
                if (DictionaryCrystall.ContainsKey(name) && DictionaryCrystall[name] != 0)
                {
                    StartCoroutine(WaitEffect());
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

    private IEnumerator WaitEffect()
    {
        Effect.SetActive(true);
        yield return new WaitForSeconds(1);
        Effect.SetActive(false);
    }

    private void UpdateUICrystall()
    {
        TextAmountCrystalls[0].text = DictionaryCrystall[Glade].ToString();
        TextAmountCrystalls[1].text = DictionaryCrystall[Rock].ToString();
        TextAmountCrystalls[2].text = DictionaryCrystall[Leo].ToString();
        for (int i = 0; i < _childrenCrystallImage.Length; i++)
        {
            string name = _childrenCrystallImage[i].gameObject.name;
            if (DictionaryCrystall[name] == 0)
            {
                Color color = _childrenCrystallImage[i].color;
                color.a = 255f;
                _childrenCrystallImage[i].color = color;
            }
        }
    }

    private void SaveAndChangeValueCrysatll()
    {
        int SaveGladeCr = PlayerPrefs.GetInt($"GladeCrystal{ParentObject.name}");
        int SaveRockCr = PlayerPrefs.GetInt($"RockCrystal{ParentObject.name}");
        int SaveLeoCr = PlayerPrefs.GetInt($"LeoCrystal{ParentObject.name}");
        switch (ParentObject.GetComponent<IMovebleObject>().Data.TypeGameObject)
        {
            case TypeGameObject.FirstPart:
                {
                    if (!PlayerPrefs.HasKey($"GladeCrystal{ParentObject.name}"))
                        ChangeDataCrystall(5, 1, 1);
                    else
                        ChangeDataCrystall(SaveGladeCr, SaveRockCr, SaveLeoCr);
                    break;
                }
            case TypeGameObject.SecondPart:
                {
                    if (!PlayerPrefs.HasKey($"RockCrystal{ParentObject.name}"))
                        ChangeDataCrystall(1, 5, 1);
                    else
                        ChangeDataCrystall(SaveGladeCr, SaveRockCr, SaveLeoCr);
                    break;
                }
            case TypeGameObject.ThirdPart:
                {
                    if (!PlayerPrefs.HasKey($"LeoCrystal{ParentObject.name}"))
                        ChangeDataCrystall(1, 1, 5);
                    else
                        ChangeDataCrystall(SaveGladeCr, SaveRockCr, SaveLeoCr);
                    break;
                }
        }
    }

    private void ChangeDataCrystall(int GladeCrystal, int RockCrystal, int LeoCrystal)
    {
        DictionaryCrystall[Glade] = GladeCrystal;
        DictionaryCrystall[Rock] = RockCrystal;
        DictionaryCrystall[Leo] = LeoCrystal;
    }

    private void AmountDataCrystall(int GladeCrystal, int RockCrystal, int LeoCrystal, string nameCrystall)
    {
        CountCrystall = nameCrystall switch
        {
            Glade => GladeCrystal,
            Rock => RockCrystal,
            Leo => LeoCrystal,
            _ => CountCrystall
        };
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
                            AmountDataCrystall(5, 1, 1, name);
                            AmountCrystal += CountCrystall - DictionaryCrystall[name];
                            break;
                        }
                    case TypeGameObject.SecondPart:
                        {
                            AmountDataCrystall(1, 5, 1, name);
                            AmountCrystal += CountCrystall - DictionaryCrystall[name];
                            break;
                        }
                    case TypeGameObject.ThirdPart:
                        {
                            AmountDataCrystall(1, 1, 5, name);
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
        PlayerPrefs.SetInt($"GladeCrystal{ParentObject.name}", DictionaryCrystall[Glade]);
        PlayerPrefs.SetInt($"RockCrystal{ParentObject.name}", DictionaryCrystall[Rock]);
        PlayerPrefs.SetInt($"LeoCrystal{ParentObject.name}", DictionaryCrystall[Leo]);
        PlayerPrefs.Save();
    }
}
