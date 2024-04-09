using UnityEngine;
using System.Collections;

public class DisableIfFarAway : MonoBehaviour
{
    private const float TIMEMISTAKE = 0.1f;

    private void Start()
    {
        StartCoroutine(AddToList());
    }

    private void OnDestroy()
    {
        ItemActivator.ActivatorItemsRemove(gameObject);
    }

    private IEnumerator AddToList()
    {
        yield return new WaitForSeconds(TIMEMISTAKE);

        ItemActivator.ActivatorItemsAdd(gameObject);
    }
}