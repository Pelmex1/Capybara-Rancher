using UnityEngine;

public class ChangeGrassMod : MonoBehaviour
{
    [SerializeField] private GameObject[] Grasses = new GameObject[3];

    private EventBus eventBus;

    private void OnEnable()
    {
        eventBus = EventBus.eventBus;
        eventBus.ChnageGrassMod += OnGrass;
    }

    private void OnDisable()
    {
        eventBus.ChnageGrassMod -= OnGrass;
    }

    private void OnGrass()
    {
        int indexQuality = QualitySettings.GetQualityLevel();
        string Quality = QualitySettings.names[indexQuality];
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
                Grasses[2].SetActive(true);
                Grasses[0].SetActive(true);
                Grasses[1].SetActive(true);
                break;
        }
    }
}
