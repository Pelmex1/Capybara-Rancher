using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MobsAi : MonoBehaviour//, IMobsAi
{
    private const float MINTIMETOFIENDNEWTARGET = 5f;
    private const float MAXTIMETOFIENDNEWTARGET = 20f;
    private const float RADIUSTOFINDTARGET = 5f;
    private const string TERRITORYOFMAPTAG = "TerritoryOfMap";
    private const string OBSTACLETAG = "Obstacle";
    private const string ANIMATORKAYFORRUNING = "IsRunning";

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
        _animator.SetBool(ANIMATORKAYFORRUNING, _agent.velocity.magnitude > 0.1f);
    }
    private Vector3 RandomPosition()
    {
        float posX = Random.Range(transform.position.x + RADIUSTOFINDTARGET, transform.position.x - RADIUSTOFINDTARGET);
        float posZ = Random.Range(transform.position.z + RADIUSTOFINDTARGET, transform.position.z - RADIUSTOFINDTARGET);
        Vector3 pos = new(posX, transform.position.y, posZ);
        return pos;
    }
    public Vector3 FoundTarget()
    {
        Vector3 pos = RandomPosition();
        Collider[] colliders = Physics.OverlapSphere( pos,0.1f, 1 << LayerMask.NameToLayer("Default"));

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(TERRITORYOFMAPTAG) && !collider.CompareTag(OBSTACLETAG))
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
            yield return new WaitForSecondsRealtime(Random.Range(MINTIMETOFIENDNEWTARGET, MAXTIMETOFIENDNEWTARGET));
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
