using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGrassMode : MonoBehaviour
{
    [SerializeField] private GameObject[] Grasses = new GameObject[3];

    private void Awake()
    {
        OnGrass();
    }

    public void OnGrass()
    {
        int indexQuality = QualitySettings.GetQualityLevel();
        string Quality = QualitySettings.names[indexQuality];
        switch (Quality)
        {
            case "Low":
                Grasses[0].SetActive(true);
                break;
            case "Medium":
                Grasses[1].SetActive(true);
                Grasses[2].SetActive(true);
                break;
            case "High":
                Grasses[2].SetActive(true);
                Grasses[0].SetActive(true);
                Grasses[1].SetActive(true);
                break;
        }
    }
}
