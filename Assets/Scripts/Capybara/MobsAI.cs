using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MobsAi : MonoBehaviour
{
    private NavMeshAgent agent;
    public bool isfoodfound {get; set;} = false;

    private Animator animator;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    private void Start() 
    {
        StartCoroutine(Moving());
    }
    private void Update()
    {
        animator.SetBool("IsRunning", agent.velocity.magnitude > 0.1f);
    }
    private Vector3 RandomPosition()
    {
        float radius = 5f;
        float posX = Random.Range(transform.position.x + radius, transform.position.x - radius);
        float posZ = Random.Range(transform.position.z + radius, transform.position.z - radius);
        Vector3 pos = new(posX, transform.position.y, posZ);
        return pos;
    }
    public Vector3 FoundTarget()
    {
        Vector3 pos = RandomPosition();
        Collider[] colliders = Physics.OverlapSphere( pos,0.1f, 1 << LayerMask.NameToLayer("Default"));

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("TerritoryOfMap") && !collider.CompareTag("Obstacle"))
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
            if (!isfoodfound && Time.timeScale == 1f)
            {
                if(agent.enabled)
                    agent.SetDestination(FoundTarget());
            }
            if(!agent.enabled)
            {
                yield return new WaitForSecondsRealtime(1f);
                continue;            
            }
            yield return new WaitForSecondsRealtime(Random.Range(5f, 20f));
        }
    }
    private void OnEnable()
    {
        StartCoroutine(Moving());
    }

    public void IsFoodFound(Transform foodTransform)
    {
        if (agent.enabled)
        {
            isfoodfound = true;
            agent.SetDestination(foodTransform.position);
        }
    }
    
}
