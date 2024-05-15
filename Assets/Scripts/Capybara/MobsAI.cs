using System.Collections;
using CapybaraRancher.Interfaces;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class MobsAi : MonoBehaviour, IMobsAi
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

    private Vector3 FoundTarget()
    {
        int maxAttempts = 10;
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 pos = RandomPosition();
            Vector3 raycastDirection = Vector3.down;
            Debug.DrawRay(pos, raycastDirection, Color.red, 0.1f);
            if (Physics.Raycast(pos, raycastDirection, out RaycastHit hit, 0.1f, 1, QueryTriggerInteraction.Ignore))
            {
                if (hit.collider.CompareTag(TERRITORY_OF_MAP_TAG))
                {
                    return pos;
                }
            }
        }
        return transform.position;
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
            if (!_isfoodfound && Time.timeScale == 1f)
            {
                if(_agent.enabled == true)
                {
                    try
                    {
                        _agent.SetDestination(RandomPosition());
                    }
                    catch
                    {
                        Debug.Log("Catch");
                        NavMeshHit hit;
                        if (NavMesh.SamplePosition(_agent.transform.position, out hit, 5.0f, NavMesh.AllAreas))
                            _agent.Warp(hit.position);
                    }
                }
            }
            yield return new WaitForSecondsRealtime(Random.Range(MIN_INTERVAL_NEW_TARGET, MAX_INTERVAL_NEW_TARGET));
        }
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
