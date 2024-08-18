using UnityEngine;
using System.Collections.Generic;
using CapybaraRancher.EventBus;

public class ItemActivator : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private float _renderDistance;

    public static List<GameObject> ActivatorItems = new();
    private void OnEnable()
    {
        EventBus.ChangeRendering += ChangeRenderDistance;
        EventBus.ActivatorItemsAdd += ActivatorItemsAdd;
        EventBus.ActivatorItemsRemove += ActivatorItemsRemove;
    }
    private void OnDisable()
    {
        EventBus.ChangeRendering -= ChangeRenderDistance;
        EventBus.ActivatorItemsAdd -= ActivatorItemsAdd;
        EventBus.ActivatorItemsRemove -= ActivatorItemsRemove;
    }

    private void ChangeRenderDistance()
    {
        _renderDistance = Camera.main.farClipPlane;
    }
    private void Start()
    {
        ChangeRenderDistance();
    }

    private void LateUpdate()
    {
        if (ActivatorItems.Count > 0)
        {
            for (int i = 0; i < ActivatorItems.Count; i++)
            {
                if (ActivatorItems[i] == null)
                {
                    ActivatorItems.Remove(ActivatorItems[i]);
                    continue;
                }
                if (Vector3.Distance(_player.transform.position, ActivatorItems[i].transform.position) > _renderDistance)
                    ActivatorItems[i].SetActive(false);
                else if (ActivatorItems[i].activeInHierarchy == false)
                {
                    ActivatorItems[i].SetActive(true);
                }
            }
        }
    }
    public void ActivatorItemsAdd(GameObject addObject)
    {
        ActivatorItems.Add(addObject);
    }
    public void ActivatorItemsRemove(GameObject removeObject)
    {
        ActivatorItems.Remove(removeObject);
    }
}