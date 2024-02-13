using UnityEngine;
using TMPro;

public class UIBilding : MonoBehaviour
{
    [SerializeField] public int IndexPlace;
    [SerializeField] private Transform PlacePosition;
    [SerializeField] private GameObject FirstObject;
    [SerializeField] private TMP_Text InfoText;

    [SerializeField] private GameObject MainButtonPanel;
    [SerializeField] private GameObject FarmPanel;
    [SerializeField] private GameObject EnclosurePanel;
    [SerializeField] public MainPanelBuilding mainPanelBuilding;
    private bool tryon = false;


    private void Update()
    {
        if (tryon)
        {
            Cursor.lockState = CursorLockMode.Confined;
            OnUi();
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
            if (!PlayerPrefs.HasKey(IndexPlace.ToString()))
            {
                MainButtonPanel.SetActive(true);
                mainPanelBuilding.IndexPlace = IndexPlace;
                mainPanelBuilding.PositionPlace = PlacePosition;
                mainPanelBuilding.FirstPlace = FirstObject;
            }
            else
            {
                string ValueKey = PlayerPrefs.GetString(IndexPlace.ToString());
                if (ValueKey == "FruitTreeArea")
                {
                    FarmPanel.SetActive(true);
                    mainPanelBuilding.IndexPlace = IndexPlace;
                    mainPanelBuilding.PositionPlace = PlacePosition;
                }
                else
                {
                    EnclosurePanel.SetActive(true);
                    mainPanelBuilding.IndexPlace = IndexPlace;
                    mainPanelBuilding.PositionPlace = PlacePosition;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InfoText.text = "";
            tryon=false;
            Cursor.lockState = CursorLockMode.Locked;
            OffBuilding();
        }
    }
}