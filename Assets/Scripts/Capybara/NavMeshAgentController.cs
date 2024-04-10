using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentController : MonoBehaviour
{
    private const string GROUND_TAG = "Ground";

    private NavMeshAgent _agent;
    private Rigidbody _rb;
    private float _raycastDistance = 0.2f;

    public bool IsGrounded { get; set; }
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        CheckIsGrounded();
        ChangeComponents();
    }
    private void ChangeComponents()
    {
        _rb.useGravity = !IsGrounded;
        _rb.isKinematic = IsGrounded;
        _agent.enabled = IsGrounded;
        
    }
    private void CheckIsGrounded()
    {
        Vector3 raycastOrigin = transform.position;
        Vector3 raycastDirection = Vector3.down;
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin, raycastDirection, out hit, _raycastDistance))
        {
            if (hit.collider.CompareTag(GROUND_TAG))
                IsGrounded = true;
            else if (hit.collider == null)
                IsGrounded = false;
        }
        else
            IsGrounded = false;
    }
}
