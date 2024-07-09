using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ChickenMoveAI : MonoBehaviour
{
    private const float MIN_INTERVAL_NEW_TARGET = 3f;
    private const float MAX_INTERVAL_NEW_TARGET = 10f;
    private const float RADIUS_OF_TARGET = 5f;
    private const string ANIMATOR_KEY_FOR_RUNING = "IsRunning";

    private NavMeshAgent _agent;
    private Animator _animator;
    private float onMeshThreshold = 3;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(Moving());
    }

    private void OnEnable()
    {
        StartCoroutine(Moving());
    }

    private void Update()
    {
        _animator.SetBool(ANIMATOR_KEY_FOR_RUNING, _agent.velocity.magnitude > 0.1f);
    }

    private Vector3 RandomPosition()
    {
        float posX = Random.Range(transform.position.x + RADIUS_OF_TARGET, transform.position.x - RADIUS_OF_TARGET);
        float posZ = Random.Range(transform.position.z + RADIUS_OF_TARGET, transform.position.z - RADIUS_OF_TARGET);
        Vector3 pos = new(posX, transform.position.y, posZ);
        return pos;
    }
    private IEnumerator Moving()
    {
        while (true)
        {
            if (!_agent.enabled)
            {
                yield return new WaitForSecondsRealtime(1f);
                continue;
            }
            yield return new WaitForSecondsRealtime(Time.deltaTime);
            if (Time.timeScale == 1f && IsAgentOnNavMesh() && _agent.enabled)
            {
                _agent.SetDestination(RandomPosition());
            }
            yield return new WaitForSecondsRealtime(Random.Range(MIN_INTERVAL_NEW_TARGET, MAX_INTERVAL_NEW_TARGET));
        }
    }
    private bool IsAgentOnNavMesh()
    {
        Vector3 agentPosition = transform.position;
        if (NavMesh.SamplePosition(agentPosition, out NavMeshHit hit, onMeshThreshold, NavMesh.AllAreas))
        {
            return agentPosition.y - hit.position.y < onMeshThreshold;
        }
        return false;
    }
}