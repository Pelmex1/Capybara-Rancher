using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemActivator : MonoBehaviour
{
    private const float TIMEMISTAKE = 0.01f;

    [SerializeField] private int _distanceFromPlayer;
    [SerializeField] private GameObject _player;

    public List<GameObject> ActivatorItems;

    private void Awake()
    {
        _distanceFromPlayer = (int)(Camera.main.farClipPlane);
        ItemActivatorHolder.ItemActivator = this;
    }
    private void Start()
    {
        StartCoroutine(CheckActivation());
    }

    private IEnumerator CheckActivation()
    {
        while (true)
        {
            if (ActivatorItems.Count > 0)
            {
                for (int i = 0; i < ActivatorItems.Count; i++)
                {
                    if (Vector3.Distance(_player.transform.position, ActivatorItems[i].transform.position) > _distanceFromPlayer)
                        ActivatorItems[i].SetActive(false);
                    else
                        ActivatorItems[i].SetActive(true);

                    yield return new WaitForSeconds(TIMEMISTAKE);
                }
            }

            yield return new WaitForSeconds(TIMEMISTAKE);
        }
    }
}