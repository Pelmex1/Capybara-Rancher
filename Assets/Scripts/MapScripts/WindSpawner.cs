using System.Collections;
using UnityEngine;

public class WindSpawner : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float intervalBetweenWindGenerete = 2f;
    [SerializeField] private GameObject[] winds;

    private void Start()
    {
        StartCoroutine(StartWindSpawn());
    }

    private IEnumerator StartWindSpawn()
    {
        while (true)
        {
            Vector3 spawnPos = player.position;
            spawnPos.x += Random.Range(-30f, 30f);
            spawnPos.y += Random.Range(5f, 10f);
            spawnPos.z += Random.Range(-30f, 30f);

            GameObject spawnedWind = Instantiate(winds[Random.Range(0, winds.Length)], spawnPos, Quaternion.identity);
            spawnedWind.transform.parent = transform;

            Destroy(spawnedWind, 5f);

            yield return new WaitForSecondsRealtime(intervalBetweenWindGenerete);
        }
    }
}
