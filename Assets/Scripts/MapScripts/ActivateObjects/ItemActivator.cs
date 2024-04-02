using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemActivator : MonoBehaviour
{
    [SerializeField] private int distanceFromPlayer;
    [SerializeField] private GameObject player;

    public List<GameObject> activatorItems;

    private void Awake()
    {
        distanceFromPlayer = (int)(Camera.main.farClipPlane);
        ItemActivatorHolder.itemActivator = this;
    }
    private void Start()
    {
        StartCoroutine(CheckActivation());
    }

    private IEnumerator CheckActivation()
    {
        while (true)
        {
            if (activatorItems.Count > 0)
            {
                for (int i = 0; i < activatorItems.Count; i++)
                {
                    if (Vector3.Distance(player.transform.position, activatorItems[i].transform.position) > distanceFromPlayer)
                        activatorItems[i].SetActive(false);
                    else
                        activatorItems[i].SetActive(true);

                    yield return new WaitForSeconds(0.01f);
                }
            }

            yield return new WaitForSeconds(0.01f);
        }
    }
}