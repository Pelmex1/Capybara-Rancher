using UnityEngine;

public class WallScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision!");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision Player!");
            GetComponent<Collider>().isTrigger = true;
        }
        else
        {
            Debug.Log("Collision Capybara!");
            GetComponent<Collider>().isTrigger = false;
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Capybara"))
        {
            Debug.Log("Collider Capybara!");
            GetComponent<Collider>().isTrigger = false;
        }
    }
}
