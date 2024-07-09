using System.Collections;
using CapybaraRancher.Interfaces;
using UnityEngine;
using UnityEngine.AI;

public class MobsAi : MonoBehaviour, IMobsAi
{
    private const float MIN_INTERVAL_NEW_TARGET = 3f;
    private const float MAX_INTERVAL_NEW_TARGET = 10f;
    private const float RADIUS_OF_TARGET = 5f;
    private const string ANIMATOR_KEY_FOR_RUNNING = "IsRunning";

    private NavMeshAgent _agent;
    private Animator _animator;
    private bool _isFoodFound;
    private float onMeshThreshold = 3f;

    private void Awake()
    {
        _isFoodFound = false;
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
        _animator.SetBool(ANIMATOR_KEY_FOR_RUNNING, _agent.velocity.magnitude > 0.1f);
    }

    private Vector3 RandomPosition()
    {
        float posX = Random.Range(transform.position.x + RADIUS_OF_TARGET, transform.position.x - RADIUS_OF_TARGET);
        float posZ = Random.Range(transform.position.z + RADIUS_OF_TARGET, transform.position.z - RADIUS_OF_TARGET);
        Vector3 pos = new Vector3(posX, transform.position.y, posZ);
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
            if (!_isFoodFound && Time.timeScale == 1f && IsAgentOnNavMesh() && _agent.enabled)
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


    public void IsFoodFound(Transform foodTransform)
    {
        if (_agent.enabled)
        {
            _isFoodFound = true;
            _agent.SetDestination(foodTransform.position);
        }
    }

    public void SetFoodFound(bool input)
    {
        _isFoodFound = input;
    }
}
