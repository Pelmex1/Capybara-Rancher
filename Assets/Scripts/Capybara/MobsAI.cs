using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MobsAi : MonoBehaviour
{
    [SerializeField] private MobsControl mobsControl;
    [SerializeField] private CapybaraData capybaraData;
    private NavMeshAgent agent;
    private bool isfoodfound = false;
    private float speedUP = 1f;
    private float delayBeforeSpawnCrystal = 2f;
    private void Start() 
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Moving());
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("movebleObject")){
            StartCoroutine(CreatePoint());
            Destroy(other.gameObject);
        }
    }
    private Vector3 RandomPosition(){
        Transform currentPoint = mobsControl.MobsLocations[Random.Range(0,mobsControl.MobsLocations.Length)];
        return new Vector3(currentPoint.position.x / Random.Range(2,4),currentPoint.position.y,currentPoint.position.z / Random.Range(2,4));
        //return new Vector3(currentPoint.position.x,currentPoint.position.y,currentPoint.position.z);
    }
    private IEnumerator Moving(){
        if(!isfoodfound){
            agent.SetDestination(RandomPosition());
        }
        yield return new WaitForSecondsRealtime(5);
        StartCoroutine(Moving());
    }
    public void IsFoodFound(Vector3 pos){
        isfoodfound = true;
        agent.SetDestination(pos);
    }
    private IEnumerator CreatePoint(){
        yield return new WaitForSecondsRealtime(delayBeforeSpawnCrystal);
        Rigidbody localrb = Instantiate(capybaraData.Point,transform.position,Quaternion.identity).GetComponent<Rigidbody>();
        localrb.MovePosition(localrb.position + speedUP * Time.deltaTime * transform.up);
        isfoodfound = false;
    }
}
