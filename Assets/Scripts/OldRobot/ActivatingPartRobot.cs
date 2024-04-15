using System;
using System.Collections.Generic;
using CustomEventBus;
using UnityEngine;

public class ActivatingPartRobot : MonoBehaviour
{
    private Dictionary<String,int> CheckDataCrusyl = new Dictionary<string, int>();
    private int AmountCrystal;

    private void Awake()
    {
        CheckDataCrusyl.Add("GladeCrystal", 0);
        CheckDataCrusyl.Add("LeoCrystal", 0);
        CheckDataCrusyl.Add("RockCrystal", 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "movebleObject"  && CheckDataCrusyl[other.name]== 0)
        {
            Destroy(other.gameObject);
            CheckDataCrusyl[other.name]++;
            CheckActiveCrystal();
        }
        else
           return;
    }

    private void CheckActiveCrystal()
    {
        foreach(var item in CheckDataCrusyl)
        {
            if(item.Value == 1 & AmountCrystal !=3)
                AmountCrystal++;
        }
        if(AmountCrystal == 3)  
            gameObject.tag = "movebleObject";
            EventBus.OnMovebleObject.Invoke(gameObject.name);
    }
}
