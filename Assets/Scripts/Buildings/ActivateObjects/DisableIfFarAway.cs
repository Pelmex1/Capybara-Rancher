using UnityEngine;
using System.Collections;
using CapybaraRancher.EventBus;
using CapybaraRancher.Consts;

public class DisableIfFarAway : MonoBehaviour
{
    private bool _isObjectSpawner;
    private void OnEnable()
    {
        StartCoroutine(AddToList());
    }

    private void OnDisable()
    {
        if (_isObjectSpawner)
            EventBus.ActivatorItemsRemove.Invoke(gameObject);
    }

    private IEnumerator AddToList()
    {
        yield return new WaitForSeconds(Constants.TIMEMISTAKE);

        EventBus.ActivatorItemsAdd.Invoke(gameObject);
    }
}