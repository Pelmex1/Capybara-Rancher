using UnityEngine;
using CustomEventBus;
using TMPro;

public class ChangeGrassMod : MonoBehaviour
{
    [SerializeField] private GameObject[] Grasses = new GameObject[3];

    [SerializeField] private TMP_Text Graghtext;

    private void OnEnable() => EventBus.ChnageGrassMod += OnGrass;
    private void OnDisable() => EventBus.ChnageGrassMod -= OnGrass;
    private void OnGrass()
    {
        int indexQuality = QualitySettings.GetQualityLevel();
        string Quality = QualitySettings.names[indexQuality];
        Graghtext.text = Quality;
        switch (Quality)
        {
            case "Low":
                Grasses[0].SetActive(true);
                Grasses[1].SetActive(false);
                Grasses[2].SetActive(false);
                break;
            case "Medium":
                Grasses[0].SetActive(true);
                Grasses[1].SetActive(true);
                Grasses[2].SetActive(false);
                break;
            case "High":
                Grasses[0].SetActive(true);
                Grasses[1].SetActive(true);
                Grasses[2].SetActive(true);
                break;
        }
    }
}

