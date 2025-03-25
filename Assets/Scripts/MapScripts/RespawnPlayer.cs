using System.Collections;
using CapybaraRancher.EventBus;
using UnityEngine;
using CapybaraRancher.Consts;

public class RespawnPlayer : MonoBehaviour
{

    [SerializeField] private Transform _spawnTransform;
    [SerializeField] private float _respawnDelay = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.PLAYER_TAG))
        {
            StartCoroutine(Respawn(other.gameObject));
        }
    }
    private IEnumerator Respawn(GameObject player)
    {
        EventBus.PlayerRespawned.Invoke();

        yield return new WaitForSeconds(_respawnDelay);

        player.transform.SetPositionAndRotation(_spawnTransform.position, _spawnTransform.rotation);
    }
}
