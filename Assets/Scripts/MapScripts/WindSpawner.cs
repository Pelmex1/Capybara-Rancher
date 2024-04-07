using System.Collections;
using UnityEngine;

public class WindSpawner : MonoBehaviour
{
    private const float MIN_VARIATION_DISTANCE_FOR_X_AND_Z = -30f;
    private const float MAX_VARIATION_DISTANCE_FOR_X_AND_Z = 30f;
    private const float MIN_VARIATION_DISTANCE_FOR_Y = 5f;
    private const float MAX_VARIATION_DISTANCE_FOR_Y = 10f;
    private const float DESTROY_DELAY = 5f;

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
            spawnPos.x += Random.Range(MIN_VARIATION_DISTANCE_FOR_X_AND_Z, MAX_VARIATION_DISTANCE_FOR_X_AND_Z);
            spawnPos.y += Random.Range(MIN_VARIATION_DISTANCE_FOR_Y, MAX_VARIATION_DISTANCE_FOR_Y);
            spawnPos.z += Random.Range(MIN_VARIATION_DISTANCE_FOR_X_AND_Z, MAX_VARIATION_DISTANCE_FOR_X_AND_Z);

            GameObject spawnedWind = Instantiate(_winds[Random.Range(0, _winds.Length - 1)], spawnPos, Quaternion.identity);
            spawnedWind.transform.parent = transform;

            Destroy(spawnedWind, DESTROY_DELAY);

            yield return new WaitForSecondsRealtime(_intervalBetweenWindGenerete);
        }
    }
}
