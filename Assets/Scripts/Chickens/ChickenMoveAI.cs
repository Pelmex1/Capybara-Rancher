using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ChickenMoveAI : MonoBehaviour
{
    private const float MIN_INTERVAL_NEW_TARGET = 5f;
    private const float MAX_INTERVAL_NEW_TARGET = 20f;
    private const float RADIUS_OF_TARGET = 5f;
    private const string TERRITORY_OF_MAP_TAG = "TerritoryOfMap";
    private const string OBSTACLE_TAG = "Obstacle";
    private const string ANIMATOR_KEY_FOR_RUNING = "IsRunning";

    private NavMeshAgent _agent;
    private Animator _animator;
    private bool _isfoodfound;
    private float onMeshThreshold = 3;

    private void Awake()
    {
        _isfoodfound = false;
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
            if (!_isfoodfound && Time.timeScale == 1f && IsAgentOnNavMesh(gameObject))
            {
                if (_agent.enabled == true && IsAgentOnNavMesh(gameObject))
                    _agent.SetDestination(RandomPosition());
            }
            yield return new WaitForSecondsRealtime(Random.Range(MIN_INTERVAL_NEW_TARGET, MAX_INTERVAL_NEW_TARGET));
        }
    }
    private bool IsAgentOnNavMesh(GameObject agentObject)
    {
        Vector3 agentPosition = agentObject.transform.position;
        if (NavMesh.SamplePosition(agentPosition, out NavMeshHit hit, onMeshThreshold, NavMesh.AllAreas))
        {
            if (Mathf.Approximately(agentPosition.x, hit.position.x) && Mathf.Approximately(agentPosition.z, hit.position.z))
                return agentPosition.y >= hit.position.y;
        }
        return false;
    }

    public void IsFoodFound(Transform foodTransform)
    {
        if (_agent.enabled)
        {
            _isfoodfound = true;
            _agent.SetDestination(foodTransform.position);
        }
    }

    public void SetFoodFound(bool _input)
    {
        _isfoodfound = _input;
    }
}