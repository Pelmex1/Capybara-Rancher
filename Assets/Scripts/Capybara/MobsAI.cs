using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MobsAi : MonoBehaviour//, IMobsAi
{
    private const float MIN_TIME_TO_FIEND_NEW_TARGET = 5f;
    private const float MAX_TIME_OF_FIND_NEW_TARGET = 20f;
    private const float RADIUS_OF_TARGET = 5f;
    private const string TERRITORY_OF_MAP_TAG = "TerritoryOfMap";
    private const string OBSTACLE_TAG = "Obstacle";
    private const string ANIMATORKAY_FORRUNING = "IsRunning";

    private NavMeshAgent _agent;
    private Animator _animator;
    private bool _isfoodfound {get; set;} = false;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }
    private void Start() 
    {
        StartCoroutine(Moving());
    }
    private void Update()
    {
        _animator.SetBool(ANIMATORKAY_FORRUNING, _agent.velocity.magnitude > 0.1f);
    }
    private Vector3 RandomPosition()
    {
        float posX = Random.Range(transform.position.x + RADIUS_OF_TARGET, transform.position.x - RADIUS_OF_TARGET);
        float posZ = Random.Range(transform.position.z + RADIUS_OF_TARGET, transform.position.z - RADIUS_OF_TARGET);
        Vector3 pos = new(posX, transform.position.y, posZ);
        return pos;
    }
    public Vector3 FoundTarget()
    {
        Vector3 pos = RandomPosition();
        Collider[] colliders = Physics.OverlapSphere( pos,0.1f, 1 << LayerMask.NameToLayer("Default"));

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
            if (!_isfoodfound && Time.timeScale == 1f)
            {
                if(_agent.enabled == true)
                    _agent.SetDestination(FoundTarget());
            }
            if(!_agent.enabled)
            {
                yield return new WaitForSecondsRealtime(1f);
                continue;            
            }
            yield return new WaitForSecondsRealtime(Random.Range(MIN_TIME_TO_FIEND_NEW_TARGET, MAX_TIME_OF_FIND_NEW_TARGET));
        }
    }
    private void OnEnable()
    {
        StartCoroutine(Moving());
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
