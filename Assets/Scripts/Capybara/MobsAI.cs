using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class MobsAi : MonoBehaviour
{
    [SerializeField] private CapybaraItem capybaraData;
    private NavMeshAgent agent;
    private bool isfoodfound = false;
    private GameObject newCrystal;
    private MovebleObject movebleObject;
    private bool hasTransformed = false;

    private Animator animator;
    private void Start() 
    {
        animator = GetComponent<Animator>();
        movebleObject = GetComponent<MovebleObject>();
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Moving());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("movebleObject"))
        {
            if (other.gameObject.GetComponent<FoodSpoilage>() != null && other.gameObject.GetComponent<Rigidbody>().isKinematic == false)
            {
                Debug.Log("1");
                StartCoroutine(GenerateCrystals());
                Destroy(other.gameObject);
            }
            else
            {
                CrystalItem dataCr = other.gameObject.GetComponent<CrystalItem>();
                InventoryItem dataIn = other.gameObject.GetComponent<MovebleObject>().data;
                if (dataCr.price != 0 && (capybaraData.crystalPrefab != dataIn.prefab && newCrystal != dataIn.prefab) && !hasTransformed)
                // This is a check to see if the crystal belongs to the kind of this capybara
                {
                    Debug.Log("2");
                    TransformationToAnotherCapybara(dataIn.prefab, dataCr.nextCapibara);
                    Destroy(other.gameObject);
                }
            }
        }
    }
    private void TransformationToAnotherCapybara(GameObject newCrystal, GameObject modification)
    {
        transform.localScale *= 1.5f;
        this.newCrystal = newCrystal;
        GameObject mod = Instantiate(modification, transform);
        mod.transform.localPosition = Vector3.zero;
        movebleObject.enabled = false;
        tag = "Untagged";
        hasTransformed = true;
    }
    private Vector3 RandomPosition()
    {
        float radius = 5f;
        Vector3 pos = new(Random.Range(transform.position.x + radius, transform.position.x - radius), transform.position.y, Random.Range(transform.position.z + radius, transform.position.z - radius));
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
    private IEnumerator Moving(){
        animator.SetFloat("IsRun",0.1f);
        if(!isfoodfound){
            agent.SetDestination(FoundTarget());
        }
        yield return new WaitForSecondsRealtime(Random.Range(5f, 20f));
        animator.SetFloat("IsRun",-0.1f);
        StartCoroutine(Moving());
    }
    public void IsFoodFound(Vector3 pos){
        isfoodfound = true;
        agent.SetDestination(pos);
    }
    private IEnumerator GenerateCrystals(){
        yield return new WaitForSecondsRealtime(2f);
        Vector3 spawnPos = transform.position + new Vector3(0f, 1f, 0f);
        Instantiate(capybaraData.crystalPrefab, spawnPos, Quaternion.identity).GetComponent<Rigidbody>().AddForce(RandomForceAdd() * 0.3f, ForceMode.Impulse);
        if (hasTransformed)
        {
            Instantiate(newCrystal, spawnPos, Quaternion.identity).GetComponent<Rigidbody>().AddForce(RandomForceAdd() * 0.3f, ForceMode.Impulse);
        }
        isfoodfound = false;
    }
    private Vector3 RandomForceAdd(){
        float radius = 5f;
        Vector3 pos = new(Random.Range(transform.position.x + radius, transform.position.x - radius), transform.position.y, Random.Range(transform.position.z + radius, transform.position.z - radius));
        return pos;
    }
}
