using UnityEngine;
using System.Collections;

public class DisableIfFarAway : MonoBehaviour
{
    private ItemActivator activation;

    private void Start()
    {
        activation = ItemActivatorHolder.itemActivator;

        StartCoroutine(AddToList());
    }

    private IEnumerator AddToList()
    {
        yield return new WaitForSeconds(0.1f);
        
        activation.activatorItems.Add(gameObject);
    }

    private void OnDestroy()
    {
        activation.activatorItems.Remove(gameObject);
    }
}