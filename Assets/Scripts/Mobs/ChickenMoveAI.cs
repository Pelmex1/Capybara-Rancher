using System.Collections;
using CapybaraRancher.Consts;
using UnityEngine;
using UnityEngine.AI;

public class ChickenMoveAI : MonoBehaviour
{
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
        _animator.SetBool(Constants.ANIMATOR_KEY_FOR_RUNING, _agent.velocity.magnitude > 0.1f);
    }

    private Vector3 RandomPosition()
    {
        float posX = Random.Range(transform.position.x + Constants.RADIUS_OF_TARGET, transform.position.x - Constants.RADIUS_OF_TARGET);
        float posZ = Random.Range(transform.position.z + Constants.RADIUS_OF_TARGET, transform.position.z - Constants.RADIUS_OF_TARGET);
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
            yield return new WaitForSecondsRealtime(Random.Range(Constants.MIN_INTERVAL_NEW_TARGET, Constants.MAX_INTERVAL_NEW_TARGET));
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