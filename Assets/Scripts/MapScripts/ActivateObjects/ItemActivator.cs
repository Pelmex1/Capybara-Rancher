using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemActivator : MonoBehaviour
{
    private const float TIME_MISTAKE = 0.01f;

    [SerializeField] private GameObject _player;

    private int _distanceFromPlayer;

    public static List<GameObject> ActivatorItems = new List<GameObject>();

    private void Awake()
    {
        _distanceFromPlayer = (int)(Camera.main.farClipPlane);
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

                    yield return new WaitForSeconds(TIME_MISTAKE);
                }
            }

            yield return new WaitForSeconds(TIME_MISTAKE);
        }
    }
    public static void ActivatorItemsAdd(GameObject addObject)
    {
        ActivatorItems.Add(addObject);
    }
    public static void ActivatorItemsRemove(GameObject removeObject)
    {
        ActivatorItems.Remove(removeObject);
    }
}