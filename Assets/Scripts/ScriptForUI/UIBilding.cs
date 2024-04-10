using UnityEngine;
using TMPro;
using CustomEventBus;

public class UIBilding : MonoBehaviour
{
    [SerializeField] private GameObject FirstObject;
    [SerializeField] private TMP_Text InfoText;
    [SerializeField] private GameObject MainButtonPanel;
    [SerializeField] private GameObject FarmPanel;
    [SerializeField] private GameObject EnclosurePanel;
    [SerializeField] private GameObject[] AllBuilding = new GameObject[3];

    private bool isNear = false;

    public int IndexPlace;
    public Transform ParentPosition;
    public GameObject ParentPlace;
    public GameObject NewObject;


    private void Awake()
    {
        ParentPosition = ParentPlace.transform;
        if (PlayerPrefs.HasKey($"{IndexPlace}"))
        {
            string NameBuilding = PlayerPrefs.GetString($"{IndexPlace}");
            for (int i = 0; i < AllBuilding.Length; i++)
            {
                if (AllBuilding[i].name == NameBuilding)
                {
                    Destroy(FirstObject);
                    NewObject = Instantiate(AllBuilding[i], ParentPosition.transform);
                    if (NewObject.TryGetComponent<IReceptacle>(out var receptacle))
                            receptacle.GetData(ParentPosition, NewObject);
                    break;
                }
                else
                {
                    continue;
                }
            }
        }
    }

    private void Update()
    {
        if (isNear)
            OnUi();        
    }

    private void OnEnable() => EventBus.OffBuilding += OffBuilding;

    private void OnDisable() => EventBus.OffBuilding -= OffBuilding;

    private void OffBuilding()
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
            isNear = true;
        }
    }

    private void OnUi()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Cursor.lockState = CursorLockMode.Confined;
            EventBus.TransitionBuildingData.Invoke(IndexPlace, ParentPosition, FirstObject, NewObject);
            if (!PlayerPrefs.HasKey($"{IndexPlace}"))
            {
                MainButtonPanel.SetActive(true);
            }
            else
            {
                string ValueKey = PlayerPrefs.GetString($"{IndexPlace}");
                if (ValueKey == "Farm")                
                    FarmPanel.SetActive(true);                
                else
                    EnclosurePanel.SetActive(true);                
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InfoText.text = "";
            isNear = false;
            Cursor.lockState = CursorLockMode.Locked;
            OffBuilding();
        }
    }
}