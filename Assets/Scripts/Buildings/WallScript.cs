using UnityEngine;

public class WallScript : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    private Collider _wallCollider;

    private void Start()
    {
        _wallCollider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        _wallCollider.isTrigger = collision.gameObject.CompareTag(PLAYER_TAG);
    }
}
