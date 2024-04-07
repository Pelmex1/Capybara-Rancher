using System.Collections.Generic;
using CustomEventBus;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private BoxCollider canonEnter;
    //public GameObject Portal2;
    private readonly float speed = 3f;
    private Collider colliderCanon;
    private bool oneFunc = true;

    public List<MovebleObject> obdjectsInCollider = new();

    public bool Ienumeratorenabled { get; set; }
    private void Awake() {
        EventBus.RemoveFromList = RemoveFromList;
        EventBus.InumeratorIsEnabled = EnamuratorEnable;
    }
    private void Start()
    {
        colliderCanon = GetComponent<BoxCollider>();
    }
    private void FixedUpdate()
    {
        EventBus.PlayerGunAttraction.Invoke();
        if (Input.GetMouseButton(0) && Cursor.lockState == CursorLockMode.Locked)
        {
            if (!Ienumeratorenabled)
            {
                //Portal2.SetActive(true);
                canonEnter.enabled = true;
            }
            colliderCanon.enabled = true;
            for (int i = 0; i < obdjectsInCollider.Count; i++)
            {
                obdjectsInCollider[i].IsMoved = true;
                obdjectsInCollider[i].transform.position = Vector3.SlerpUnclamped(obdjectsInCollider[i].transform.position, canonEnter.transform.position, speed * Time.deltaTime);
                oneFunc = true;
            }
        }
        else
        {
            //Portal2.SetActive(false);
            if (oneFunc)
            {
                for (int i = 0; i < obdjectsInCollider.Count; i++)
                {
                    obdjectsInCollider[i].IsMoved = false;
                }              
                canonEnter.enabled = false;
                colliderCanon.enabled = false;
                obdjectsInCollider.Clear();
                oneFunc = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("movebleObject"))
        {
            obdjectsInCollider.Add(other.gameObject.GetComponent<MovebleObject>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("movebleObject"))
        {
            obdjectsInCollider.Remove(other.gameObject.GetComponent<MovebleObject>());
        }
    }
    private void RemoveFromList(MovebleObject movebleObject) => obdjectsInCollider.Remove(movebleObject);
    private void EnamuratorEnable(bool isEnable) => Ienumeratorenabled = isEnable;
}
