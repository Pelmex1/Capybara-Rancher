using UnityEngine;
using System.Collections;

public class DisableIfFarAway : MonoBehaviour
{
    private const float TIMEMISTAKE = 0.1f;

    private IItemActivator _activation;

    private void Start()
    {
        _activation = ItemActivatorHolder.ItemActivator;

        StartCoroutine(AddToList());
    }

    private void OnDestroy()
    {
        _activation.ActivatorItemsRemove(gameObject);
    }

    private IEnumerator AddToList()
    {
        yield return new WaitForSeconds(TIMEMISTAKE);
        
        _activation.ActivatorItemsAdd(gameObject);
    }
}