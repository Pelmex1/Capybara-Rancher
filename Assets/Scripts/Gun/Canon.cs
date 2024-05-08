using System.Collections.Generic;
using CapybaraRancher.EventBus;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private BoxCollider canonEnter;
    [SerializeField] private GameObject Portal2;
    private const float SPEED = 3f;
    private Collider _colliderCanon;
    private bool _oneFunc = true;

    public List<GameObject> obdjectsInCollider = new();
    public bool Ienumeratorenabled { get; set; } = false;
    private void Awake() {
        EventBus.RemoveFromList = (GameObject gameObject) => obdjectsInCollider.Remove(gameObject); Debug.Log("Work Remove");
        EventBus.InumeratorIsEnabled = (bool isEnable) => Ienumeratorenabled = isEnable;
        EventBus.CheckList = (GameObject check) => obdjectsInCollider.Contains(check);
    }
    private void Start()
    {
        _colliderCanon = GetComponent<BoxCollider>();
    }
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && Cursor.lockState == CursorLockMode.Locked)
        {
            if (!Ienumeratorenabled)
            {
                Portal2.SetActive(true);
                canonEnter.enabled = true;
            }
            _colliderCanon.enabled = true;
            for (int i = 0; i < obdjectsInCollider.Count; i++)
            {
                if (obdjectsInCollider[i] != null)
                    obdjectsInCollider[i].transform.position = Vector3.SlerpUnclamped(obdjectsInCollider[i].transform.position, canonEnter.transform.position, SPEED * Time.deltaTime);
                _oneFunc = true;
            }
        }
        else
        {
            canonEnter.enabled = false;
            Portal2.SetActive(false);
            if (_oneFunc)
            {
                canonEnter.enabled = false;
                _colliderCanon.enabled = false;
                obdjectsInCollider.Clear();
                _oneFunc = false;
            }
        }
        EventBus.PlayerGunAttraction.Invoke(Input.GetMouseButton(0) && Cursor.lockState == CursorLockMode.Locked);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("movebleObject"))
        {
            obdjectsInCollider.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("movebleObject"))
        {
            obdjectsInCollider.Remove(other.gameObject);
        }
    }
}
