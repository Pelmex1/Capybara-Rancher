using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentController : MonoBehaviour
{
    private const string GROUND_TAG = "Ground";
    private const float RAYCAST_LENGTH_CAPYBARA = 0.2f;
    private const float RAYCAST_LENGTH_CHICKEN = 0.1f;

    [SerializeField] private bool _isChicken = false;

    private NavMeshAgent _agent;
    private Rigidbody _rb;
    private float _raycastLength;
    private bool IsGrounded;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _rb = GetComponent<Rigidbody>();
        IsGrounded = false;
        _raycastLength = RAYCAST_LENGTH_CAPYBARA;
        if (_isChicken)
            _raycastLength = RAYCAST_LENGTH_CHICKEN;
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
