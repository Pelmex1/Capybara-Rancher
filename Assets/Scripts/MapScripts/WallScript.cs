using UnityEngine;

public class WallScript : MonoBehaviour
{
    private Collider wallCollider;
    private void Start() {
        wallCollider = GetComponent<Collider>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            wallCollider.isTrigger = true;
        }
        else
        {
            wallCollider.isTrigger = false;
        }
    }
}
