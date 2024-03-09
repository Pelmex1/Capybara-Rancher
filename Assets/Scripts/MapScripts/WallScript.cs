using UnityEngine;

public class WallScript : MonoBehaviour
{
    private Collider wallCollider;
    private void Start()
    {
        wallCollider = GetComponent<Collider>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        wallCollider.isTrigger = collision.gameObject.CompareTag("Player");
    }
}
