using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapybaraArray : MonoBehaviour
{
    [SerializeField] private GameObject capybaraPool;
    [SerializeField] private GameObject capybaraCase;
    
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent)
        if (other.GetComponent<MobsAi>() != null)
        {
            other.transform.parent = Instantiate(capybaraCase).transform;
        }
    }
}
