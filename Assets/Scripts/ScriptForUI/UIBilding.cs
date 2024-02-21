using UnityEngine;
using TMPro;

public class UIBilding : MonoBehaviour
{
    [SerializeField] private GameObject FirstObject;
    [SerializeField] private TMP_Text InfoText;
    [SerializeField] private GameObject MainButtonPanel;
    [SerializeField] private GameObject FarmPanel;
    [SerializeField] private GameObject EnclosurePanel;
    
    private bool tryon = false;

    public MainPanelBuilding mainPanelBuilding;
    public int IndexPlace;
    public Transform ParentPosition;
    public GameObject ParentPlace;
    public GameObject NewObject;


    private void Awake()
    {
        ParentPosition = ParentPlace.transform;
    }

    private void Update()
    {
        if (tryon)
        {
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
            Cursor.lockState = CursorLockMode.Confined;
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