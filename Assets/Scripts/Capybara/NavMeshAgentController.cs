using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Rigidbody rb;
    private float raycastDistance = 0.6f;
    public bool isGrounded {get; set;}
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        IsGrounded();
        ChangeComponents();
    }
    private void ChangeComponents()
    {
        if(agent.enabled){
            rb.useGravity = !isGrounded;
            rb.isKinematic = isGrounded;
            agent.enabled = isGrounded;
        }
    }
    private void IsGrounded()
    {
        Vector3 raycastOrigin = transform.position;
        raycastOrigin.y += 0.5f;
        Vector3 raycastDirection = Vector3.down;
        RaycastHit hit;
        Debug.DrawRay(raycastOrigin, raycastDirection * raycastDistance, Color.green);
        if (Physics.Raycast(raycastOrigin, raycastDirection, out hit, raycastDistance))
            if (hit.collider.CompareTag("Ground"))
                isGrounded =  true;
        else
            isGrounded =  false;
    }
}
