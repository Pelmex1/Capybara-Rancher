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
    private const string ANIMATOR_KEY_FOR_RUNNING = "IsRunning";

    private NavMeshAgent _agent;
    private Animator _animator;

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
        _animator.SetBool(ANIMATOR_KEY_FOR_RUNNING, _agent.velocity.magnitude > 0.1f);
    }

    private Vector3 RandomPosition()
    {
        float posX = Random.Range(transform.position.x + RADIUS_OF_TARGET, transform.position.x - RADIUS_OF_TARGET);
        float posZ = Random.Range(transform.position.z + RADIUS_OF_TARGET, transform.position.z - RADIUS_OF_TARGET);
        Vector3 pos = new(posX, transform.position.y, posZ);
        return pos;
    }

    private Vector3 FoundTarget()
    {
        Vector3 pos = RandomPosition();
        Collider[] colliders = Physics.OverlapSphere(pos, 0.1f, 1 << LayerMask.NameToLayer("Default"));

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(TERRITORY_OF_MAP_TAG) && !collider.CompareTag(OBSTACLE_TAG))
            {
                return pos;
            }
        }

        return FoundTarget();
    }

    private IEnumerator Moving()
    {
        while (true)
        {
            if (Time.timeScale == 1f)
            {
                if (_agent.enabled == true)
                    _agent.SetDestination(RandomPosition());
            }
            if (!_agent.enabled)
            {
                yield return new WaitForSecondsRealtime(1f);
                continue;
            }
            yield return new WaitForSecondsRealtime(Random.Range(MIN_INTERVAL_NEW_TARGET, MAX_INTERVAL_NEW_TARGET));
        }
    }
}