using CapybaraRancher.Consts;
using UnityEngine;

public class WallScript : MonoBehaviour
{

    private Collider _wallCollider;

    private void Start()
    {
        _wallCollider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        _wallCollider.isTrigger = collision.gameObject.CompareTag(Constants.PLAYER_TAG);
    }
}
