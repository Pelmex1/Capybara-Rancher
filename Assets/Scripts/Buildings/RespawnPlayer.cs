using System.Collections;
using CapybaraRancher.EventBus;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    [SerializeField] private Transform _spawnTransform;
    [SerializeField] private float _respawnDelay = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            StartCoroutine(Respawn(other.gameObject));
        }
    }
    private IEnumerator Respawn(GameObject player)
    {
        EventBus.PlayerRespawned.Invoke();

        yield return new WaitForSeconds(_respawnDelay);

        player.transform.position = _spawnTransform.position;
        player.transform.rotation = _spawnTransform.rotation;
    }
}