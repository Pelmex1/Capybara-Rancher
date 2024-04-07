using System.Collections;
using UnityEngine;

public class WindSpawner : MonoBehaviour
{
    private const float MINVARIATIONDISTANCEFORXANDZ = -30f;
    private const float MAXVARIATIONDISTANCEFORXANDZ = 30f;
    private const float MINVARIATIONDISTANCEFORY = 5f;
    private const float MAXVARIATIONDISTANCEFORY = 10f;
    private const float DESTROYDELAY = 5f;

    [SerializeField] private Transform _player;
    [SerializeField] private float _intervalBetweenWindGenerete = 2f;
    [SerializeField] private GameObject[] _winds;

    private void Start()
    {
        StartCoroutine(StartWindSpawn());
    }

    private IEnumerator StartWindSpawn()
    {
        while (true)
        {
            Vector3 spawnPos = _player.position;
            spawnPos.x += Random.Range(MINVARIATIONDISTANCEFORXANDZ, MAXVARIATIONDISTANCEFORXANDZ);
            spawnPos.y += Random.Range(MINVARIATIONDISTANCEFORY, MAXVARIATIONDISTANCEFORY);
            spawnPos.z += Random.Range(MINVARIATIONDISTANCEFORXANDZ, MAXVARIATIONDISTANCEFORXANDZ);

            GameObject spawnedWind = Instantiate(_winds[Random.Range(0, _winds.Length - 1)], spawnPos, Quaternion.identity);
            spawnedWind.transform.parent = transform;

            Destroy(spawnedWind, DESTROYDELAY);

            yield return new WaitForSecondsRealtime(_intervalBetweenWindGenerete);
        }
    }
}
