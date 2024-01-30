using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MobsAi : MonoBehaviour
{
    [SerializeField] private MobsControl mobsControl;
    private NavMeshAgent agent;
    private void Start() 
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Moving());
    }
    private Vector3 RandomPosition(){
        Transform currentPoint = mobsControl.MobsLocations[Random.Range(0,mobsControl.MobsLocations.Length)];
        return new Vector3(currentPoint.position.x / Random.Range(2,4),currentPoint.position.y,currentPoint.position.z / Random.Range(2,4));
        //return new Vector3(currentPoint.position.x,currentPoint.position.y,currentPoint.position.z);
    }
    private IEnumerator Moving(){
        agent.SetDestination(RandomPosition());
        yield return new WaitForSecondsRealtime(5);
        StartCoroutine(Moving());
    }
}
