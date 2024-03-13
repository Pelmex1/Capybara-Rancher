using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Rigidbody rb;
    public bool IsGrounded {get; set;}
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
        if(agent.enabled){
            rb.useGravity = !IsGrounded;
            rb.isKinematic = IsGrounded;
            agent.enabled = IsGrounded;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        IsGrounded = collision.gameObject.CompareTag("Ground");
    }
}
