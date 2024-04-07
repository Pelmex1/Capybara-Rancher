using UnityEngine;
using System.Collections;

public class DisableIfFarAway : MonoBehaviour
{
    private const float TIMEMISTAKE = 0.1f;

    private ItemActivator _activation;

    private void Start()
    {
        _activation = ItemActivatorHolder.ItemActivator;

        StartCoroutine(AddToList());
    }

    private void OnDestroy()
    {
        _activation.ActivatorItems.Remove(gameObject);
    }

    private IEnumerator AddToList()
    {
        yield return new WaitForSeconds(TIMEMISTAKE);
        
        _activation.ActivatorItems.Add(gameObject);
    }
}