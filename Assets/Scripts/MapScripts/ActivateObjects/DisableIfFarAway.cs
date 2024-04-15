using UnityEngine;
using System.Collections;

public class DisableIfFarAway : MonoBehaviour
{
    private const float TIMEMISTAKE = 0.1f;

    private bool _isObjectSpawner;

    private void Awake()
    {
        IObjectSpawner _objectSpawner = null;
        transform.parent?.TryGetComponent(out _objectSpawner);
        _isObjectSpawner = _objectSpawner != null;
    }

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