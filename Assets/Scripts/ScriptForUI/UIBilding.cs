using UnityEngine;
using TMPro;

public class UIBilding : MonoBehaviour
{
    public Transform ParentPosition;
    public GameObject ParentPlace;
    public GameObject NewObject;
    [SerializeField] public int IndexPlace;
    [SerializeField] private GameObject FirstObject;
    [SerializeField] private TMP_Text InfoText;

    [SerializeField] private GameObject MainButtonPanel;
    [SerializeField] private GameObject FarmPanel;
    [SerializeField] private GameObject EnclosurePanel;
    [SerializeField] public MainPanelBuilding mainPanelBuilding;
    private bool tryon = false;


    private void Awake()
    {
        ParentPosition = ParentPlace.transform;
    }

    private void Update()
    {
        if (tryon)
        {
            Cursor.lockState = CursorLockMode.Confined;
            OnUi();
            mainPanelBuilding.UIBuilding = this;
        }
    }

    public void OffBuilding()
    {
        MainButtonPanel.SetActive(false);
        FarmPanel.SetActive(false);
        EnclosurePanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InfoText.text = "Press Key E";
            tryon = true;
        }
    }

    private void OnUi()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!PlayerPrefs.HasKey($"{IndexPlace}"))
            {
                MainButtonPanel.SetActive(true);
                mainPanelBuilding.IndexPlace = IndexPlace;
                mainPanelBuilding.PositionPlace = ParentPosition;
                mainPanelBuilding.FirstPlace = FirstObject;
                NewObject = mainPanelBuilding.NewPlace;
            }
            else
            {
                string ValueKey = PlayerPrefs.GetString($"{IndexPlace}");
                if (ValueKey == "Farm")
                {
                    FarmPanel.SetActive(true);
                    mainPanelBuilding.IndexPlace = IndexPlace;
                    mainPanelBuilding.PositionPlace = ParentPosition;
                }
                else
                {
                    EnclosurePanel.SetActive(true);
                    mainPanelBuilding.IndexPlace = IndexPlace;
                    mainPanelBuilding.PositionPlace = ParentPosition;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InfoText.text = "";
            tryon = false;
            Cursor.lockState = CursorLockMode.Locked;
            OffBuilding();
        }
    }
}