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
    private FoodType whatEat1;
    private FoodType whatEat2;
    private string nameOfFavouriteFood1;
    private string nameOfFavouriteFood2;

    private Animator animator;
    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        movebleObject = GetComponent<MovebleObject>(); 
    }
    private void Start() 
    {
        StartCoroutine(Moving());

        whatEat1 = capybaraData.whatEat;
        nameOfFavouriteFood1 = capybaraData.nameOfFavouriteFood;
    }
    private void Update()
    {
        animator.SetBool("IsRunning", agent.velocity.magnitude > 0.1f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("movebleObject"))
        {
            if (collision.gameObject.GetComponent<FoodItem>() != null && collision.gameObject.GetComponent<Rigidbody>().isKinematic == false)
            {
                string nameOfFood = collision.gameObject.GetComponent<MovebleObject>().data.name;
                FoodType typeOfFood = collision.gameObject.GetComponent<FoodItem>().type;
                if (nameOfFavouriteFood1 == nameOfFood || nameOfFavouriteFood2 == nameOfFood)
                {
                    StartCoroutine(GenerateCrystals(true));
                    Destroy(collision.gameObject);
                }
                else if ((whatEat1 == FoodType.All || whatEat1 == typeOfFood) || (whatEat2 == FoodType.All || whatEat2 == typeOfFood))
                {
                    StartCoroutine(GenerateCrystals(false));
                    Destroy(collision.gameObject);
                }
            }
            else if (collision.gameObject.GetComponent<CrystalItem>() != null)
            {
                CrystalItem dataCr = collision.gameObject.GetComponent<CrystalItem>();
                InventoryItem dataIn = collision.gameObject.GetComponent<MovebleObject>().data;
                if (dataCr.price != 0 && (capybaraData.crystalPrefab != dataIn.prefab && newCrystal != dataIn.prefab) && !hasTransformed)
                {
                    Debug.Log("FoundCrystal");
                    TransformationToAnotherCapybara(dataIn.prefab, dataCr.nextCapibara, dataCr.nameOfFavouriteFoodThisType, dataCr.whatEatThisType);
                    Destroy(collision.gameObject);
                }
            }
        }
    }
    private void TransformationToAnotherCapybara(GameObject newCrystal, GameObject modification, string nameOfSecondFavouriteFood, FoodType whatEatSecond)
    {
        transform.localScale *= 1.5f;
        this.newCrystal = newCrystal;
        nameOfFavouriteFood2 = nameOfSecondFavouriteFood;
        whatEat2 = whatEatSecond;
        Instantiate(modification, transform);
        movebleObject.enabled = false;
        tag = "Untagged";
        hasTransformed = true;
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
                agent.SetDestination(FoundTarget());
            }
            yield return new WaitForSecondsRealtime(Random.Range(5f, 20f));
        }
    }

    public void IsFoodFound(Vector3 pos)
    {
        isfoodfound = true;
        agent.SetDestination(pos);
    }
    private IEnumerator GenerateCrystals(bool isFavouriteFood){
        yield return new WaitForSecondsRealtime(2f);
        Vector3 spawnPos = transform.position + new Vector3(0f, 1f, 0f);
        int crystalCount = isFavouriteFood ? 2 : 1;
        for (int i = 0; i < crystalCount; i++)
        {
            GameObject crystal1 = Instantiate(capybaraData.crystalPrefab, spawnPos, Quaternion.identity);
            crystal1.GetComponent<Rigidbody>().AddForce(RandomForceAdd() * 0.3f, ForceMode.Impulse);

            if (hasTransformed)
            {
                GameObject crystal2 = Instantiate(newCrystal, spawnPos, Quaternion.identity);
                crystal2.GetComponent<Rigidbody>().AddForce(RandomForceAdd() * 0.3f, ForceMode.Impulse);
            }
        }

        isfoodfound = false;
    }
    private Vector3 RandomForceAdd(){
        float radius = 5f;
        float posx = Random.Range(transform.position.x + radius, transform.position.x - radius);
        float posZ = Random.Range(transform.position.z + radius, transform.position.z - radius);
        Vector3 pos = new (posx, transform.position.y, posZ);
        return pos;
    }
}
