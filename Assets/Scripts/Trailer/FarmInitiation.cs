using UnityEngine;

public class FarmInitiation : MonoBehaviour
{
    [SerializeField] private GameObject _farm;
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.SetActive(false);
        _farm.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }
}
