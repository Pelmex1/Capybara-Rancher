using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Rigidbody rb;
    private bool isGrounded;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        ChangeComponents();
    }
    private void ChangeComponents()
    {
        rb.useGravity = !isGrounded;
        rb.isKinematic = isGrounded;
        agent.enabled = isGrounded;
    }
    private void OnCollisionEnter(Collision collision)
    {
        try
        {
            if (collision.gameObject.CompareTag("Ground"))
               isGrounded = true;
            else
                isGrounded = false;
        }
        finally
        {

        }
    }
}
