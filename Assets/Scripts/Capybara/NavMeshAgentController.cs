using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Rigidbody _rb;
    private bool IsGrounded;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _rb = GetComponent<Rigidbody>();
        IsGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        CheckIsGrounded();
        ChangeComponents();
    }

    private void OnCollisionExit(Collision collision)
    {
        CheckIsGrounded();
        ChangeComponents();
    }
    private void OnTriggerEnter(Collider other)
    {
        CheckIsGrounded();
        ChangeComponents();
    }
    private void OnTriggerExit(Collider other)
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
        _agent.enabled = true;
        if (_agent.isOnNavMesh)
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
            _agent.enabled = false;
        }
    }
}
