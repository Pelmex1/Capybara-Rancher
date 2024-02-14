using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class MobsAi : MonoBehaviour
{
    [SerializeField] private MobsControl mobsControl;
    [SerializeField] private InventoryItem capybaraData;
    private NavMeshAgent agent;
    private bool isfoodfound = false;
    private readonly float delayBeforeSpawnCrystal = 2f;
    private readonly GameObject[] crystalsThisKind = new GameObject[2];
    private MovebleObject movebleObject;
    private bool hasTransformed = false;
    private void Start() 
    {
        movebleObject = GetComponent<MovebleObject>();
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Moving());

        crystalsThisKind[0] = capybaraData.point;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("movebleObject"))
        {
            InventoryItem data = other.gameObject.GetComponent<MovebleObject>().data;
            if (other.gameObject.GetComponent<FoodSpoilage>() != null && other.gameObject.GetComponent<Rigidbody>().isKinematic == false)
            {
                StartCoroutine(GenerateCrystals());
                Destroy(other.gameObject);
            }
            else if (data.minPrice != 0 && !crystalsThisKind.Contains(data.prefab) && !hasTransformed)
                // This is a check to see if the crystal belongs to the kind of this capybara
            {
                TransformationToAnotherCapybara(data.prefab, data.modThisKind);
                Destroy(other.gameObject);
            }
        }
    }
    private void TransformationToAnotherCapybara(GameObject newCrystal, GameObject modification)
    {
        transform.localScale *= 1.5f;
        crystalsThisKind[1] = newCrystal;
        GameObject mod = Instantiate(modification, transform);
        mod.transform.localPosition = Vector3.zero;
        movebleObject.enabled = false;
        tag = "Untagged";
        hasTransformed = true;
    }
    private Vector3 RandomPosition()
    {
        float radius = 5f;
        bool pointIsCorrect = false;
        Vector3 pos;
        pos = Vector3.zero;
        while (!pointIsCorrect)
        {
            pos = new Vector3(Random.Range(transform.position.x + radius, transform.position.x - radius), transform.position.y, Random.Range(transform.position.z + radius, transform.position.z - radius));
            pointIsCorrect = CheckForCollidersAtPosition(pos);
        }
        return pos;
        /*
        Transform currentPoint = mobsControl.MobsLocations[Random.Range(0,mobsControl.MobsLocations.Length)];
        return new Vector3(currentPoint.position.x / Random.Range(2,4),currentPoint.position.y,currentPoint.position.z / Random.Range(2,4));
        //return new Vector3(currentPoint.position.x,currentPoint.position.y,currentPoint.position.z);
        */
    }
    public bool CheckForCollidersAtPosition(Vector3 pos)
    {
        Collider[] colliders = Physics.OverlapSphere(pos, 0.1f, 1 << LayerMask.NameToLayer("Default"));

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("TerritoryOfMap") && !collider.CompareTag("Obstacle"))
            {
                return true;
            }
        }

        return false;
    }
    private IEnumerator Moving(){
        if(!isfoodfound){
            agent.SetDestination(RandomPosition());
        }
        yield return new WaitForSecondsRealtime(Random.Range(5f, 20f));
        StartCoroutine(Moving());
    }
    public void IsFoodFound(Vector3 pos){
        isfoodfound = true;
        agent.SetDestination(pos);
    }
    private IEnumerator GenerateCrystals(){
        yield return new WaitForSecondsRealtime(delayBeforeSpawnCrystal);
        Vector3 spawnPos = transform.position + new Vector3(0f, 1f, 0f);
        Instantiate(crystalsThisKind[0], spawnPos, Quaternion.identity);
        if (hasTransformed)
        {
            Instantiate(crystalsThisKind[1], spawnPos, Quaternion.identity);
        }
        isfoodfound = false;
    }
}
