using System.Collections.Generic;
using CapybaraRancher.Enums;
using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class ActivateBuildRobot : MonoBehaviour, ITransitionCrystallData
{
    public bool WasChangeDict { get; set; } = false;
    public Dictionary<string, int> DictionaryCrystall { get; set; } = new();
    [SerializeField] private Image[] _childrenCrystallImage = new Image[3];
    private GameObject ParentObject;
    IRobotParts _irobotspart;
    ICrystall _icrystall;

    private void Start()
    {
        ParentObject = gameObject.transform.parent.gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (ParentObject.TryGetComponent(out _irobotspart) && other.TryGetComponent(out _icrystall))
        {
            if (_irobotspart.CheckMoving == false && _icrystall != null)
            {
                EventBus.AddToDict.Invoke();

                switch (ParentObject.GetComponent<IMovebleObject>().Data.TypeGameObject)
                {
                    case TypeGameObject.FirstPart:
                        {
                            DictionaryCrystall["GladeCrystal"] = 5;
                            break;
                        }
                    case TypeGameObject.SecondPart:
                        {
                            DictionaryCrystall["RockCrystal"] = 5;
                            break;
                        }
                    case TypeGameObject.ThirdPart:
                        {
                            DictionaryCrystall["LeoCrystal"] = 5;
                            break;
                        }
                }
                string name = _icrystall.NameCrystal;
                if (DictionaryCrystall.ContainsKey(name) && DictionaryCrystall[name] == 0)
                {
                    Destroy(other.gameObject);
                    DictionaryCrystall[name]++;
                    CheckActiveCrystal();
                }
            }

        }
        else
            return;
    }

    private void CheckActiveCrystal()
    {
        int AmountCrystal = 0;
        for (int i = 0; i < _childrenCrystallImage.Length; i++)
        {
            string name = _childrenCrystallImage[i].gameObject.name;
            if (DictionaryCrystall.ContainsKey(name))
            {
                if (DictionaryCrystall[name] == 1)
                {
                    Color color = _childrenCrystallImage[i].color;
                    color.a = 255f;
                    _childrenCrystallImage[i].color = color;
                    AmountCrystal++;
                }
            }
        }
        // foreach (var item in CheckDataCrusyl)
        // {
        //     if (item.Value == 1)
        //         AmountCrystal++;
        // }
        if (AmountCrystal == 3)
        {
            _irobotspart.WasBuilding = true;
            EventBus.WasAddingAllCrystall.Invoke();
        }

    }
}
