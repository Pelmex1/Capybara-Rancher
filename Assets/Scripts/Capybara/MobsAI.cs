using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MobsAi : MonoBehaviour
{
    [SerializeField] private MobsControl mobsControl;
    [SerializeField] private InventoryItem capybaraData;
    [SerializeField] private GameObject targetPointPrefab;
    private NavMeshAgent agent;
    private bool isfoodfound = false;
    private float speedUP = 1f;
    private float delayBeforeSpawnCrystal = 2f;
    private GameObject[] crystalsThisKind = new GameObject[2];
    private void Start() 
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Moving());

        crystalsThisKind[0] = capybaraData.point;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("movebleObject"))
        {
            if (other.gameObject.GetComponent<FoodSpoilage>() != null && other.gameObject.GetComponent<Rigidbody>() == false)
            {
                StartCoroutine(GenerateCrystals());
                Destroy(other.gameObject);
            }
            else if (other.gameObject.GetComponent<MovebleObject>().data.minPrice != 0 &&
                (other.gameObject.GetComponent<MovebleObject>().data.prefab != crystalsThisKind[0] && other.gameObject.GetComponent<MovebleObject>().data.prefab != crystalsThisKind[1]))
                // This is a check to see if the crystal belongs to the kind of this capybara
            {
                TransformationToAnotherCapybara(other.gameObject.GetComponent<MovebleObject>().data.prefab, other.gameObject.GetComponent<MovebleObject>().data.modThisKind);
                Destroy(other.gameObject);
            }
        }
    }
    private void TransformationToAnotherCapybara(GameObject newCrystal, GameObject modification)
    {
        transform.localScale = new Vector3(transform.localScale.x * 1.5f, transform.localScale.y * 1.5f, transform.localScale.z * 1.5f);
        crystalsThisKind[1] = newCrystal;
        GameObject mod = Instantiate(modification, Vector3.zero, Quaternion.identity, transform);
        mod.transform.localPosition = Vector3.zero;
        mod.transform.localRotation = Quaternion.identity;
        GetComponent<MovebleObject>().enabled = false;
        tag = "Untagged";
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
        Rigidbody localrb = Instantiate(crystalsThisKind[0], transform.position,Quaternion.identity).GetComponent<Rigidbody>();
        localrb.MovePosition(localrb.position + speedUP * Time.deltaTime * transform.up);
        if (crystalsThisKind[1] != null)
        {
            localrb = Instantiate(crystalsThisKind[1], transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            localrb.MovePosition(localrb.position + speedUP * Time.deltaTime * transform.up);
        }
        isfoodfound = false;
    }
}
