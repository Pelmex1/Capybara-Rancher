using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private BoxCollider canonEnter;
    private readonly float speed = 3f;
    private Collider colliderCanon;

    public List<Transform> obdjectsInCollider = new();
    public bool isIenumeratorenabled;
    private void Start()
    {
        colliderCanon = GetComponent<BoxCollider>();
    }
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if (!isIenumeratorenabled)
            {
                canonEnter.enabled = true;
            }
            colliderCanon.enabled = true;
            for (int i = 0; i < obdjectsInCollider.Count; i++)
            {
                obdjectsInCollider[i].position = Vector3.SlerpUnclamped(obdjectsInCollider[i].position, canonEnter.transform.position, speed * Time.deltaTime);
            }
        }
        else
        {
            canonEnter.enabled = false;
            colliderCanon.enabled = false;
            obdjectsInCollider.Clear();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("movebleObject"))
        {
            obdjectsInCollider.Add(other.gameObject.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("movebleObject"))
        {
            obdjectsInCollider.Remove(other.gameObject.transform);
        }
    }
}
