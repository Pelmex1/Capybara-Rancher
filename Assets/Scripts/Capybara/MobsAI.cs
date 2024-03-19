using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class MobsAi : MonoBehaviour
{
    private CapybaraItem capybaraData;
    private NavMeshAgent agent;
    private MovebleObject movebleObject;
    private FoodType whatEat1;
    private FoodType whatEat2;
    private string nameOfFavouriteFood1;
    private string nameOfFavouriteFood2;
    private CrystalsController crystalsController;
    private bool hasTransformed {get; set;} = false;
    public bool isfoodfound {get; set;} = false ;

    private Animator animator;
    private void Awake() {
        capybaraData = GetComponent<CapybaraItem>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        movebleObject = GetComponent<MovebleObject>(); 
        crystalsController =  GetComponent<CrystalsController>(); 
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
            if (collision.gameObject.GetComponent<FoodItem>() != null && collision.gameObject.GetComponent<Rigidbody>().isKinematic == false && crystalsController.isHungry)
            {
                string nameOfFood = collision.gameObject.GetComponent<MovebleObject>().data.name;
                FoodType typeOfFood = collision.gameObject.GetComponent<FoodItem>().type;
                if (nameOfFavouriteFood1 == nameOfFood || nameOfFavouriteFood2 == nameOfFood)
                {
                    StartCoroutine(crystalsController.GenerateCrystals(true));
                    Destroy(collision.gameObject);
                }
                else if ((whatEat1 == FoodType.All || whatEat1 == typeOfFood) || (whatEat2 == FoodType.All || whatEat2 == typeOfFood))
                {
                    StartCoroutine(crystalsController.GenerateCrystals(false));
                    Destroy(collision.gameObject);
                }
            }
            else if (collision.gameObject.GetComponent<CrystalItem>() != null)
            {
                CrystalItem dataCr = collision.gameObject.GetComponent<CrystalItem>();
                InventoryItem dataIn = collision.gameObject.GetComponent<MovebleObject>().data;
                if (dataCr.price != 0 && (capybaraData.crystalPrefab != dataIn.prefab && crystalsController.newCrystal != dataIn.prefab) && !hasTransformed)
                {
                    TransformationToAnotherCapybara(dataIn.prefab, dataCr.nextCapibara, dataCr.nameOfFavouriteFoodThisType, dataCr.whatEatThisType);
                    Destroy(collision.gameObject);
                }
            }
        }
    }
    private void TransformationToAnotherCapybara(GameObject newCrystal, GameObject modification, string nameOfSecondFavouriteFood, FoodType whatEatSecond)
    {
        transform.localScale *= 1.5f;
        crystalsController.newCrystal = newCrystal;
        nameOfFavouriteFood2 = nameOfSecondFavouriteFood;
        whatEat2 = whatEatSecond;
        Instantiate(modification, transform);
        movebleObject.enabled = false;
        tag = "Untagged";
        crystalsController.hasTransformed = true;
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

    public void IsFoodFound(Transform foodTransform)
    {
        if (agent.enabled)
        {
            isfoodfound = true;
            agent.SetDestination(foodTransform.position);
        }
    }
    
}
