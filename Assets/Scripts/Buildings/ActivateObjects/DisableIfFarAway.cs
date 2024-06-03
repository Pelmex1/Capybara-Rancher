using UnityEngine;
using System.Collections;
using CapybaraRancher.Interfaces;

public class DisableIfFarAway : MonoBehaviour
{
    private const float TIMEMISTAKE = 0.1f;

    private bool _isObjectSpawner;
    private void OnEnable()
    {
        StartCoroutine(AddToList());
    }

    private void OnDisable()
    {
        if (_isObjectSpawner)
            ItemActivator.ActivatorItemsRemove(gameObject);
    }

    private IEnumerator AddToList()
    {
        yield return new WaitForSeconds(TIMEMISTAKE);

        ItemActivator.ActivatorItemsAdd(gameObject);
    }
}